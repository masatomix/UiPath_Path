using Google.Apis.Auth.OAuth2;
using System;
using System.Activities;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace Utils.StringUtils
{

    public sealed class GoogleAuthUtil : CodeActivity
    {

        //[Category("Input")]
        //[RequiredArgument]
        //public InArgument<IDictionary<string, object>> Payload { get; set; }


        //[Category("Input")]
        //[RequiredArgument]
        //public InArgument<string> PrivateKey { get; set; }


        //[Category("Input")]
        //public InArgument<IDictionary<string, object>> ExtraHeaders { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> JsonPath { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string[]> Scopes { get; set; }


        [Category("Output")]
        public OutArgument<string> Bearer { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            //var payload = context.GetValue(this.Payload);
            //var privateKey = context.GetValue(this.PrivateKey);
            //var extraHeaders = context.GetValue(this.ExtraHeaders);
            //var token = CreateToken(payload, privateKey, extraHeaders);

            var jsonPath = context.GetValue(this.JsonPath);
            var scopes = context.GetValue(this.Scopes);


            GoogleCredential credential;
            using (Stream stream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                credential = GoogleCredential.FromStream(stream);
            }

            //credential = credential.CreateScoped(new[] {
            //    "https://www.googleapis.com/auth/dialogflow"
            //});

            credential = credential.CreateScoped(scopes);

            string bearer;
            try
            {
                Task<string> task = ((ITokenAccess)credential).GetAccessTokenForRequestAsync();
                task.Wait();
                bearer = task.Result;
                context.SetValue(Bearer, bearer);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        // C#上では動くが、UiPathのActivityにしたら、動かない。謎。
        //private static string CreateToken(object payload, string privateKey, IDictionary<string, object> extraHeaders = null, JwtSettings settings = null, JwtOptions options = null)
        //{
        //    Console.WriteLine(payload);
        //    Console.WriteLine(extraHeaders);
        //    Console.WriteLine(privateKey);

        //    string jwt = string.Empty;
        //    RsaPrivateCrtKeyParameters keyParameters;
        //    using (var sr = new StringReader(privateKey))
        //    {
        //        var pr = new PemReader(sr);
        //        keyParameters = (RsaPrivateCrtKeyParameters)pr.ReadObject();
        //    }
        //    RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(keyParameters);
        //    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        //    {
        //        rsa.ImportParameters(rsaParams);
        //        jwt = JWT.Encode(payload, rsa, JwsAlgorithm.RS256, extraHeaders, settings, options);
        //    }
        //    return jwt;
        //}
    }
}
