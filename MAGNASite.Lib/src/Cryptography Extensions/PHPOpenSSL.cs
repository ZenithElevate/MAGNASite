/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

using OpenSSL.Crypto;
using System.Text;

namespace MAGNASite.Lib
{
    internal enum OpenSslPurposeCheckingFlags
    {
        X509_PURPOSE_SSL_CLIENT = 1, // 
        X509_PURPOSE_SSL_SERVER = 2, // 
        X509_PURPOSE_NS_SSL_SERVER = 3, // 
        X509_PURPOSE_SMIME_SIGN = 4, // 
        X509_PURPOSE_SMIME_ENCRYPT = 5, // 
        X509_PURPOSE_CRL_SIGN = 6, // 
        X509_PURPOSE_ANY = 7  // 
    }

    internal enum OpenSslPaddingFlags
    {
        OPENSSL_PKCS1_PADDING = 1, // 
        OPENSSL_SSLV23_PADDING = 2, // 
        OPENSSL_NO_PADDING = 3, // 
        OPENSSL_PKCS1_OAEP_PADDING = 4, // 
    }

    internal enum OpenSslKeyTypes
    {
        OPENSSL_KEYTYPE_RSA = 0, //                                                                              
        OPENSSL_KEYTYPE_DSA = 1, //                                                                              
        OPENSSL_KEYTYPE_DH = 2, //                                                                               
        OPENSSL_KEYTYPE_EC = 3, //     This constant is only available when PHP is compiled with OpenSSL 0.9.8+. 
    }
    internal enum OpenSslPKCS7Flags
    {
        PKCS7_TEXT = 1, // Adds text/plain content type headers to encrypted/signed message. If decrypting or verifying, it strips those headers from the output - if the decrypted or verified message is not of MIME type text/plain then an error will occur.                                                                                                                                        
        PKCS7_BINARY = 128, // Normally the input message is converted to "canonical" format which is effectively using CR and LF as end of line: as required by the S/MIME specification. When this option is present, no translation occurs. This is useful when handling binary data which may not be in MIME format.                                                                                        
        PKCS7_NOINTERN = 16, // When verifying a message, certificates (if any) included in the message are normally searched for the signing certificate. With this option only the certificates specified in the extracerts parameter of openssl_pkcs7_verify() are used. The supplied certificates can still be used as untrusted CAs however.                                                            
        PKCS7_NOVERIFY = 32, // Do not verify the signers certificate of a signed message.                                                                                                                                                                                                                                                                                                                   
        PKCS7_NOCHAIN = 8, // Do not chain verification of signers certificates: that is don't use the certificates in the signed message as untrusted CAs.                                                                                                                                                                                                                                                
        PKCS7_NOCERTS = 2, // When signing a message the signer's certificate is normally included - with this option it is excluded. This will reduce the size of the signed message but the verifier must have a copy of the signers certificate available locally (passed using the extracerts to openssl_pkcs7_verify() for example).                                                                  
        PKCS7_NOATTR = 256, // Normally when a message is signed, a set of attributes are included which include the signing time and the supported symmetric algorithms. With this option they are not included.                                                                                                                                                                                               
        PKCS7_DETACHED = 64, // When signing a message, use cleartext signing with the MIME type "multipart/signed". This is the default if you do not specify any flags to openssl_pkcs7_sign(). If you turn this option off, the message will be signed using opaque signing, which is more resistant to translation by mail relays but cannot be read by mail agents that do not support S/MIME.          
        PKCS7_NOSIGS = 4, // Don't try and verify the signatures on a message                                                                                                                                                                                                                                                                                                                                 
    }

    internal enum OpenSslSignatureAlgorithms
    {
        OPENSSL_ALGO_DSS1 = 5, //                                                                                                                                                                                                     
        OPENSSL_ALGO_SHA1 = 1, //     Used as default algorithm by openssl_sign() and openssl_verify().                                                                                                                               
        OPENSSL_ALGO_SHA224 = 6, //                                                                                                                                                                                                     
        OPENSSL_ALGO_SHA256 = 7, //                                                                                                                                                                                                     
        OPENSSL_ALGO_SHA384 = 8, //                                                                                                                                                                                                     
        OPENSSL_ALGO_SHA512 = 9, //                                                                                                                                                                                                     
        OPENSSL_ALGO_RMD160 = 10, //                                                                                                                                                                                                     
        OPENSSL_ALGO_MD5 = 2, //                                                                                                                                                                                                     
        OPENSSL_ALGO_MD4 = 3, //                                                                                                                                                                                                     
        //Notice:  Use of undefined constant OPENSSL_ALGO_MD2 - assumed 'OPENSSL_ALGO_MD2' in C:\WWW\Dressmaker\pi.php on line 71
        //OPENSSL_ALGO_MD2 = OPENSSL_ALGO_MD2, //     This constant is only available if PHP is compiled with MD2 support. This requires passing in the -DHAVE_OPENSSL_MD2_H CFLAG when compiling PHP, and enable-md2 when compiling OpenSSL 1.0.0+.  
    }

    internal enum OpenSslCiphers
    {
        OPENSSL_CIPHER_RC2_40 = 0, // 
        OPENSSL_CIPHER_RC2_128 = 1, // 
        OPENSSL_CIPHER_RC2_64 = 2, // 
        OPENSSL_CIPHER_DES = 3, // 
        OPENSSL_CIPHER_3DES = 4, // 
        OPENSSL_CIPHER_AES_128_CBC = 5, // 
        OPENSSL_CIPHER_AES_192_CBC = 6, // 
        OPENSSL_CIPHER_AES_256_CBC = 7, // 
    }

    internal enum OpenSslServerNameIndication
    {
        OPENSSL_TLSEXT_SERVER_NAME = 1, //  Whether SNI support is available or not. 
    }

    internal enum OpenSslOther
    {
        OPENSSL_RAW_DATA = 1, // If OPENSSL_RAW_DATA is set in the openssl_encrypt() or openssl_decrypt(), the returned data is returned as-is. When it is not specified, Base64 encoded data is returned to the caller.                                                                                                                                                                                                     
        OPENSSL_ZERO_PADDING = 2, // By default encryption operations are padded using standard block padding and the padding is checked and removed when decrypting. If OPENSSL_ZERO_PADDING is set in the openssl_encrypt() or openssl_decrypt() options then no padding is performed, the total amount of data encrypted or decrypted must then be a multiple of the block size or an error will occur.                       
        //Notice:  Use of undefined constant OPENSSL_ENCODING_SMIME - assumed 'OPENSSL_ENCODING_SMIME' in C:\WWW\Dressmaker\pi.php on line 97
        //OPENSSL_ENCODING_SMIME = OPENSSL_ENCODING_SMIME, // Indicates that encoding is S/MIME.                                                                                                                                                                                                                                                                                                                                                      
        //Notice:  Use of undefined constant OPENSSL_ENCODING_DER - assumed 'OPENSSL_ENCODING_DER' in C:\WWW\Dressmaker\pi.php on line 98
        //OPENSSL_ENCODING_DER = OPENSSL_ENCODING_DER, // Indicates that encoding is DER (Distinguished Encoding Rules).                                                                                                                                                                                                                                                                                                                          
        //Notice:  Use of undefined constant OPENSSL_ENCODING_PEM - assumed 'OPENSSL_ENCODING_PEM' in C:\WWW\Dressmaker\pi.php on line 99
        //OPENSSL_ENCODING_PEM = OPENSSL_ENCODING_PEM, // Indicates that encoding is PEM (Privacy-Enhanced Mail).                                                                                                                                                                                                                                                                                                                                 
    }

    /// <summary>
    /// 
    /// </summary>
    internal class OpenSSLAsymmetricKey
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class OpenSSLCertificate
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// OpenSSL
    /// </summary>
    internal class PHPOpenSSL
    {
        static Dictionary<int, string> MessageDigestMethods, CipherMethods;

        public const string OPENSSL_VERSION_TEXT = "OpenSSL 1.0.2g  1 Mar 2016"; //  
        public const int OPENSSL_VERSION_NUMBER = 268443775; //  

        public static void openssl_cipher_iv_length() { }//Gets the cipher iv length
        public static void openssl_cms_decrypt() { }//Decrypt a CMS message
        public static void openssl_cms_encrypt() { }//Encrypt a CMS message
        public static void openssl_cms_read() { }//Export the CMS file to an array of PEM certificates
        public static void openssl_cms_sign() { }//Sign a file
        public static void openssl_cms_verify() { }//Verify a CMS signature
        public static void openssl_csr_export_to_file() { }//Exports a CSR to a file
        public static void openssl_csr_export() { }//Exports a CSR as a string
        public static void openssl_csr_get_public_key() { }//Returns the public key of a CSR
        public static void openssl_csr_get_subject() { }//Returns the subject of a CSR
        public static void openssl_csr_new() { }//Generates a CSR
        public static void openssl_csr_sign() { }//Sign a CSR with another certificate(or itself) and generate a certificate

        /// <summary>
        /// Decrypts data
        /// </summary>
        /// <param name="data">The encrypted message to be decrypted.</param>
        /// <param name="cipher_algo">The cipher method. For a list of available cipher methods, use openssl_get_cipher_methods().</param>
        /// <param name="passphrase">The key.</param>
        /// <param name="options">options can be one of OPENSSL_RAW_DATA, OPENSSL_ZERO_PADDING.</param>
        /// <param name="iv">A non-NULL Initialization Vector.</param>
        /// <param name="tag">The authentication tag in AEAD cipher mode. If it is incorrect, the authentication fails and the function returns false.</param>
        /// <param name="aad">Additional authenticated data.</param>
        /// <returns>The decrypted string on success or false on failure.</returns>
        public static (string, bool) openssl_decrypt(string data, string cipher_algo, string passphrase, int options = 0, string iv = "", string tag = null, string aad = "")
        {
            if (data == null)
            {
                return (null, false);
            }

        }

        public static void openssl_dh_compute_key() { }//Computes shared secret for public value of remote DH public key and local DH key

        /// <summary>
        /// Computes a digest
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="digest_algo">The digest method to use, e.g. "sha256", see openssl_get_md_methods() for a list of available digest methods.</param>
        /// <param name="binary">Setting to true will return as raw output data, otherwise the return value is binhex encoded.</param>
        /// <returns>Returns the digested hash value on success or false on failure.</returns>
        public static (string, bool) openssl_digest(string data, string digest_algo, bool binary = false)
        {
            if (data == null)
            {
                return (null, false);
            }

            MessageDigest dg = null;
            try
            {
                switch (digest_algo) // TODO: Cache it?
                {
                    case "MD4":
                        dg = MessageDigest.MD4;
                        break;
                    case "MD5":
                        dg = MessageDigest.MD5;
                        break;
                    case "SHA":
                        dg = MessageDigest.SHA;
                        break;
                    case "SHA1":
                        dg = MessageDigest.SHA1;
                        break;
                    case "SHA224":
                        dg = MessageDigest.SHA224;
                        break;
                    case "SHA256":
                        dg = MessageDigest.SHA256;
                        break;
                    case "SHA384":
                        dg = MessageDigest.SHA384;
                        break;
                    case "SHA512":
                        dg = MessageDigest.SHA512;
                        break;
                    case "DSS":
                        dg = MessageDigest.DSS;
                        break;
                    case "DSS1":
                        dg = MessageDigest.DSS1;
                        break;
                    case "RipeMD160":
                        dg = MessageDigest.RipeMD160;
                        break;
                    case "ECDSA":
                        dg = MessageDigest.ECDSA;
                        break;
                    default:
                        dg = MessageDigest.CreateByName(digest_algo);
                        break;
                }

                using (var mdc = new MessageDigestContext(dg))
                {
                    var result = mdc.Digest(binary ? Encoding.Default.GetBytes(data) : Convert.FromBase64String(data));
                    return (binary ? Encoding.Default.GetString(result) : Convert.ToBase64String(result), true);
                }
            }
            finally
            {
                if (dg != null)
                {
                    try
                    {
                        dg.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void openssl_encrypt() { }//Encrypts data
        public static void openssl_error_string() { }//Return openSSL error message
        public static void openssl_free_key() { }//Free key resource
        public static void openssl_get_cert_locations() { }//Retrieve the available certificate locations

        /// <summary>
        /// Gets available cipher methods
        /// </summary>
        /// <param name="aliases">Set to true if cipher aliases should be included within the returned array.</param>
        /// <returns>An array of available cipher methods. Note that prior to OpenSSL 1.1.1, the cipher methods have been returned in upper case and lower case spelling; as of OpenSSL 1.1.1 only the lower case variants are returned.</returns>
        public static Dictionary<int, string> openssl_get_cipher_methods(bool aliases = false)
        {
            if (CipherMethods == null)
            {
                var i = 0;
                CipherMethods = Cipher.AllNamesSorted.ToDictionary(x => i++, x => x);
            }

            return CipherMethods;
        }

        public static void openssl_get_curve_names() { }//Gets list of available curve names for ECC

        /// <summary>
        /// Gets available digest methods
        /// </summary>
        /// <returns>An array of available digest methods.</returns>
        public static Dictionary<int, string> openssl_get_md_methods()
        {
            if (MessageDigestMethods == null)
            {
                var i = 0;
                MessageDigest.AllNamesSorted.ToDictionary(x => i++, x => x);
            }

            return MessageDigestMethods;
        }

        public static void openssl_get_privatekey() { }//Alias of openssl_pkey_get_private
        public static void openssl_get_publickey() { }//Alias of openssl_pkey_get_public

        //Open sealed data
        public static bool openssl_open(string data, ref string output, string encrypted_key, OpenSSLAsymmetricKey private_key, string cipher_algo, string iv = null)
        {
            return false;
        }

        public static bool openssl_open(string data, ref string output, string encrypted_key,    OpenSSLCertificate private_key, string cipher_algo, string iv = null)
        {
            return false;
        }

        public static bool openssl_open(string data, ref string output, string encrypted_key,    object[] private_key, string cipher_algo, string iv = null)
        {
            return false;
        }

        public static bool openssl_open(string data, ref string output, string encrypted_key,    string private_key, string cipher_algo, string iv = null)
        {
            return false;
        }


        public static void openssl_pbkdf2() { }//Generates a PKCS5 v2 PBKDF2 string
        public static void openssl_pkcs12_export_to_file() { }//Exports a PKCS#12 Compatible Certificate Store File
        public static void openssl_pkcs12_export() { }//Exports a PKCS#12 Compatible Certificate Store File to variable
        public static void openssl_pkcs12_read() { }//Parse a PKCS#12 Certificate Store into an array
        public static void openssl_pkcs7_decrypt() { }//Decrypts an S/MIME encrypted message
        public static void openssl_pkcs7_encrypt() { }//Encrypt an S/MIME message
        public static void openssl_pkcs7_read() { }//Export the PKCS7 file to an array of PEM certificates
        public static void openssl_pkcs7_sign() { }//Sign an S/MIME message
        public static void openssl_pkcs7_verify() { }//Verifies the signature of an S/MIME signed message
        public static void openssl_pkey_derive() { }//Computes shared secret for public value of remote and local DH or ECDH key
        public static void openssl_pkey_export_to_file() { }//Gets an exportable representation of a key into a file
        public static void openssl_pkey_export() { }//Gets an exportable representation of a key into a string
        public static void openssl_pkey_free() { }//Frees a private key
        public static void openssl_pkey_get_details() { }//Returns an array with the key details

        public static OpenSSLAsymmetricKey openssl_pkey_get_private(OpenSSLAsymmetricKey private_key, string passphrase = null)
        {
            return null;
        }//Get a private key

        public static OpenSSLAsymmetricKey openssl_pkey_get_private(OpenSSLCertificate private_key, string passphrase = null)
        {
            return null;
        }//Get a private key

        public static OpenSSLAsymmetricKey openssl_pkey_get_private(object[] private_key, string passphrase = null)
        {
            return null;
        }//Get a private key

        public static OpenSSLAsymmetricKey openssl_pkey_get_private(string private_key, string passphrase = null)
        {
            return null;
        }//Get a private key

        public static void openssl_pkey_get_public() { }//Extract public key from certificate and prepare it for use
        public static void openssl_pkey_new() { }//Generates a new private key
        public static void openssl_private_decrypt() { }//Decrypts data with private key
        public static void openssl_private_encrypt() { }//Encrypts data with private key
        public static void openssl_public_decrypt() { }//Decrypts data with public key
        public static void openssl_public_encrypt() { }//Encrypts data with public key
        public static void openssl_random_pseudo_bytes() { }//Generate a pseudo-random string of bytes
        public static void openssl_seal() { }//Seal(encrypt) data
        public static void openssl_sign() { }//Generate signature
        public static void openssl_spki_export_challenge() { }//Exports the challenge associated with a signed public key and challenge
        public static void openssl_spki_export() { }//Exports a valid PEM formatted public key signed public key and challenge
        public static void openssl_spki_new() { }//Generate a new signed public key and challenge
        public static void openssl_spki_verify() { }//Verifies a signed public key and challenge
        public static void openssl_verify() { }//Verify signature
        public static void openssl_x509_check_private_key() { }//Checks if a private key corresponds to a certificate
        public static void openssl_x509_checkpurpose() { }//Verifies if a certificate can be used for a particular purpose
        public static void openssl_x509_export_to_file() { }//Exports a certificate to file
        public static void openssl_x509_export() { }//Exports a certificate as a string
        public static void openssl_x509_fingerprint() { }//Calculates the fingerprint, or digest, of a given X.509 certificate
        public static void openssl_x509_free() { }//Free certificate resource
        public static void openssl_x509_parse() { }//Parse an X509 certificate and return the information as an array
        public static void openssl_x509_read() { }//Parse an X.509 certificate and return an object for it
        public static void openssl_x509_verify() { }//Verifies digital signature of x509 certificate against a public key
    }
}