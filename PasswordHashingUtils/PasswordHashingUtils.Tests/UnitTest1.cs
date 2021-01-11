using System;
using Xunit;
using IIG.CoSFE.DatabaseUtils;
using IIG.NEW.PasswordHashingUtils;
namespace PasswordHashingUtils.Tests
{

    public class UnitTest1
    {
        private const string server = @"localhost";
        private const string database = @"IIG.CoSWE.AuthDB";
        private const bool isTrusted = false;
        private const string login = @"sa";
        private const string password = @"Adele1*!,";
        private const int connectionTimeOut = 75;
        private const string loginPassword = "julia";
        private const string loginPassword2 = "hello";
        private const string loginPassword3 = "cats";
        private const string loginPassword4 = "exists";
        readonly AuthDatabaseUtils authDatabaseUtils = new AuthDatabaseUtils(server, database, isTrusted, login, password, connectionTimeOut);

        [Fact]
        public void AuthDB_CheckCredentials_True()
        {
            Assert.True(authDatabaseUtils.CheckCredentials(loginPassword2, loginPassword2));
        }
         [Fact]
        public void AuthDB_DeleteCredentials_True()
        {
            Assert.True(authDatabaseUtils.DeleteCredentials(loginPassword, loginPassword));
        }
         [Fact]
        public void AuthDB_UpdateCredentialsNotHashedPassword_True()
        {
            string hashedPassword = PasswordHasher.GetHash(loginPassword3);
            Assert.True(authDatabaseUtils.DeleteCredentials(loginPassword3, hashedPassword));
            Assert.True(authDatabaseUtils.AddCredentials(loginPassword3, hashedPassword));      

            Assert.False(authDatabaseUtils.UpdateCredentials(loginPassword3, hashedPassword, "newlogin", "ok"));
        }
        [Fact]
        public void AuthDB_UpdateCredentialsHashedPassword_True()
        {
            string hashedPassword = PasswordHasher.GetHash(loginPassword3);
            string hashedPasswordOk = PasswordHasher.GetHash("ok");
        
            Assert.True(authDatabaseUtils.DeleteCredentials(loginPassword3, hashedPassword));
            Assert.True(authDatabaseUtils.AddCredentials(loginPassword3, hashedPassword));      

            Assert.True(authDatabaseUtils.UpdateCredentials(loginPassword3, hashedPassword, "newlogin", hashedPasswordOk));
        }
        [Fact]
        public void AuthDB_ExecSql_AddCredentials_True()
        {
            Assert.True(authDatabaseUtils.ExecSql("INSERT INTO [IIG.CoSWE.AuthDB].[dbo].[Credentials](Login, Password) VALUES('julia','julia')"));
        }
        [Fact]
        public void AuthDB_AddCredentialsThatExists_True()
        // Was created manually
        {
            Assert.False(authDatabaseUtils.AddCredentials(loginPassword4, loginPassword4));            
        }
        [Fact]
        public void AuthDB_ExecSql_UpdateCredentials_True()
        {
            Assert.True(authDatabaseUtils.ExecSql("UPDATE [IIG.CoSWE.AuthDB].[dbo].[Credentials] SET Login = 'julia', Password = 'julia' WHERE [Login]='julia';"));            
        }
         [Fact]
        public void AuthDB_AddCredentialsWithHash_True()
        {
            
            string hashedPassword = PasswordHasher.GetHash(loginPassword3);
            Assert.True(authDatabaseUtils.DeleteCredentials(loginPassword3, hashedPassword));
            Assert.True(authDatabaseUtils.AddCredentials(loginPassword3, hashedPassword));         
            
        }
    }
}
