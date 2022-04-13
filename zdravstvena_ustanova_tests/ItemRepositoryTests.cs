using Xunit;
using Repository;
using System;

namespace zdravstvena_ustanova_tests
{
    public class ItemRepositoryTests
    {
        private static string CSV_DELIMITER = ";";
        private static string _projectPath = System.Reflection.Assembly.GetExecutingAssembly().Location
           .Split(new string[] { "bin" }, StringSplitOptions.None)[0];
        private static string ITEM_FILE = _projectPath + "\\..\\zdravstvena_ustanova\\Resources\\Data\\Items.csv";


        [Fact]
        public void ItemsFileInCorrectFormat()
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);

            Assert.NotNull(itemRepository.GetAll());

        }

        [Theory]
        [InlineData(1)]
        public void GetItemByIdExist(long id)
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);

            Assert.NotNull(itemRepository.GetById(id));

        }

        [Theory]
        [InlineData(-1)]
        public void GetItemByIdDoesNotExist(long id)
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);

            Assert.Null(itemRepository.GetById(id));

        }
    }
}