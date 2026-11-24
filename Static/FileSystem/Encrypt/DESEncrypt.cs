using System;
using System.Data.SqlTypes;
using System.Security.Cryptography;
namespace PVZEngine.Encrypt;
/// <summary>
/// 使用RSA加密数据
/// </summary>
public static class RSAEncrypt
{
    static RSAEncryptionPadding ds;
    /// 加密
    /// </summary>
    /// <returns></returns>
    public static string Encrypt(string data,string Key = null)
    {
        try{
            System.Security.Cryptography.CspParameters parameters = new();
            parameters.KeyContainerName = Key ?? "zhiqiang";
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new(parameters))
            {
                byte[] Plaindata = System.Text.Encoding.Default.GetBytes(data);
                byte[] encryptdata = rsa.Encrypt(Plaindata,false);
                return Convert.ToBase64String(encryptdata);
            }
        }catch(Exception EX)
        {
            DEBUG.Info.PrintErr(EX);
        }
        finally
        {
        }
        return "Error";
    }
    /// <summary>
    /// 解密
    /// </summary>
    /// <returns></returns>
    public static string Decrypt(string data,string Key = null)
    {
        try{
            System.Security.Cryptography.CspParameters parameters = new();
            parameters.KeyContainerName = Key ?? "zhiqiang";
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new(parameters))
            {
                byte[] Plaindata = Convert.FromBase64String(data); 
                byte[] encryptdata = rsa.Decrypt(Plaindata,false);
                return System.Text.Encoding.Default.GetString(encryptdata);
            }
        }catch(Exception EX)
        {
            DEBUG.Info.PrintErr(EX);
        }
        finally
        {
            
        }
        return "Error";
    }
}