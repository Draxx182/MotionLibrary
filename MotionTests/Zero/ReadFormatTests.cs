using MotionLibrary;
using System;
using System.Reflection;

namespace MotionTests.Zero
{
    public class ReadFormatTests
    {
        private const string _TestFilesPath = @"Zero\Input";
        private const string _YakuzaZeroPropertyPath = @"zero-property.bin";
        private OldEngineFormat _yakuzaZeroFile;

        /// <summary>
        /// Reads and intializes a property.bin into objects for unit testing.
        /// </summary>
        /// <exception cref="FileNotFoundException">File was not found in executing directory.</exception>
        public ReadFormatTests()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(baseDir, _TestFilesPath, _YakuzaZeroPropertyPath);

            var file = OldEngineFormat.Read(path);
            if (file == null)
            {
                Console.WriteLine(path);
                throw new FileNotFoundException();
            }

            _yakuzaZeroFile = file;
        }

        [Fact]
        public void ReadFrom_DataTables_CorrectMoveAmount()
        {
            int actual = _yakuzaZeroFile.Moves.Count;
            const int Expected = 9079;

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void ReadFrom_DataTables_CorrectMEPAmount()
        {
            int actual = _yakuzaZeroFile.MEPs.Count;
            const int Expected = 9078;

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void ReadFrom_DataTables_CorrectGMTAmount()
        {
            int actual = _yakuzaZeroFile.GMTs.Count;
            const int Expected = 8497;

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void ReadList_MEPs_CorrectFirstItem()
        {
            string actual = _yakuzaZeroFile.MEPs.First();
            const string Expected = "A_FSH_result_anago";

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void ReadList_GMTs_CorrectFirstItem()
        {
            string actual = _yakuzaZeroFile.GMTs.First();
            const string Expected = "A_CAT_MOV_run";

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void ReadList_MEPs_CorrectLastItem()
        {
            string actual = _yakuzaZeroFile.MEPs.Last();
            const string Expected = "E_btlst_NSK_B15";

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void ReadList_GMTs_CorrectLastItem()
        {
            string actual = _yakuzaZeroFile.GMTs.Last();
            const string Expected = "W_MNK_WPY_stand";

            Assert.Equal(Expected, actual);
        }
    }
}
