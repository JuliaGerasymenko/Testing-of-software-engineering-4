using System;
using Xunit;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;
using IIG.FileWorker;

namespace BinaryFlag.Tests
{
    public class UnitTest1
    {
        private const string Server = @"localhost";
        private const string Database = @"IIG.CoSWE.FlagPoleDB";
        private const string database = @"IIG.CoSWE.AuthDB";
        private const bool isTrusted = false;
        private const string Login = @"sa";
        private const string password = @"Adele1*!,";
        private const int ConnectionTimeout = 75;
        private string dirName = "BinaryTestDir";
        const string addTxt = "//" + "test.txt";
        private string txtFilePath = "home/julia/vscode/BinaryFlag/BinaryFlag.Tests/bin/Debug/netcoreapp3.1/BinaryDirTest1" + addTxt;
        readonly DatabaseUtils databaseUtils = new DatabaseUtils(Server, Database, isTrusted, Login, password, ConnectionTimeout);
       
        [Fact]
        public void FileWorker_GetFullPath_True()
        {        
            var folderPath = BaseFileWorker.MkDir(dirName);
            Assert.True(BaseFileWorker.GetFullPath(folderPath) != null);
        }
        [Fact]
        public void FileWorker_GetPath_True()
        {        
            var folderPath = BaseFileWorker.MkDir(dirName);
            Assert.True(BaseFileWorker.GetPath(folderPath) != null);
        }
        [Fact]
        public void FileWorker_TryWrite_True()
        {        
            var folderPath = BaseFileWorker.MkDir(dirName);
            Assert.True(BaseFileWorker.TryWrite("hello", folderPath + addTxt));
        }
        [Fact]
        public void FileWorker_Write_True()
        {        
            var folderPath = BaseFileWorker.MkDir(dirName);
            Assert.True(BaseFileWorker.Write("hello", folderPath + addTxt));
        }
        [Fact]
        public void FileWorker_TryCopy_True()
        {        
            var folderPath = BaseFileWorker.MkDir(dirName);
            string filePath = folderPath + addTxt;
            Assert.True(BaseFileWorker.Write("hello", filePath));
            Assert.True(BaseFileWorker.TryCopy(filePath, folderPath+"//"+"bye.txt", true));
        }
       
        [Fact]
        public void FileWorker_GetFileName_True()
        {        
            var folderPath = BaseFileWorker.MkDir(dirName);
            string filePath = folderPath + addTxt;
            Assert.True(BaseFileWorker.Write("hello", filePath));
            Assert.True(BaseFileWorker.GetFileName(filePath)!= null);
        }
        [Fact]
        public void FileWorker_MkDir_True()
        {        
            var folderPath = BaseFileWorker.MkDir(dirName);
            Assert.True(folderPath != null);
        }
        [Fact]
        public void FileWorker_InitialValueFalse_True()
        {
            var folderPath = BaseFileWorker.MkDir(dirName);
            if (folderPath != null) {
                string filePath = folderPath + addTxt;
                
                MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(2, false);
                string stringFlag = binaryFlag.GetFlag().ToString();
            
                bool writeFlag = BaseFileWorker.Write(stringFlag, filePath);
                if (writeFlag) {
                    Assert.Equal(stringFlag, BaseFileWorker.ReadAll(filePath));
                } else {
                   Assert.True(false); 
                }

            } else {
                Assert.True(false);
            }
        }
        [Fact]
        public void FileWorker_InitialValueTrue_True()
        {
            var folderPath = BaseFileWorker.MkDir(dirName);
            Console.WriteLine("+",BaseFileWorker.GetFullPath(folderPath));
            if (folderPath != null) {
                string filePath = folderPath + addTxt;
                
                MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(2, true);
                string stringFlag = binaryFlag.GetFlag().ToString();
            
                bool writeFlag = BaseFileWorker.Write(stringFlag, filePath);
                if (writeFlag) {
                    Assert.Equal(stringFlag, BaseFileWorker.ReadAll(filePath));
                } else {
                   Assert.True(false); 
                }

            } else {
                Assert.True(false);
            }
        }
    }
}
