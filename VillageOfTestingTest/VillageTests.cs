using System;
using VillageOfTesting;
using VillageOfTesting.Objects;
namespace VillageOfTestingTest
{
    public class VillageTests
    {
        [Fact]
        public void TestAddWorker()
        {
            var village = new Village();
            village.AddWorker("Anna", "farmer");

            Assert.Single(village.Workers);
            Assert.Equal("Anna", village.Workers[0].Name);
            Assert.Equal("farmer", village.Workers[0].Occupation);
            Assert.Contains(village.Workers, worker => worker.Name == "Anna" && worker.Occupation == "farmer");


        }

        [Fact]
        public void TestAddMultipleWorkers()
        {
            var village = new Village();
            village.AddWorker("Anna", "farmer");
            village.AddWorker("Anders", "lumberjack");

            Assert.Equal(2, village.Workers.Count);
            Assert.Equal("Anders", village.Workers[1].Name);
            Assert.Equal("lumberjack", village.Workers[1].Occupation);

        }

        [Fact]
        public void TestCannotAddMoreThanMaxWorkers()
        {
            var village = new Village();
            for (int i = 0; i < village.MaxWorkers; i++)
            {
                village.AddWorker($"Worker{i}", "farmer");
            }

            Assert.Equal(village.MaxWorkers, village.Workers.Count);

        }

        [Fact]
        public void TestDayWithoutWorkers()
        {
            var village = new Village();
            village.Day();

            Assert.Equal(1, village.DaysGone);
            Assert.False(village.GameOver);
        }

        [Fact]
        public void TestDayWithWorkersAndFood()
        {
            var village = new Village();
            village.AddWorker("Anna", "farmer");

            village.Food = 10;
            village.Day();

            Assert.Equal(1, village.DaysGone);
            Assert.True(village.Workers[0].Alive);
            Assert.Equal(14, village.Food);

        }


        [Fact]
        public void TestWorkersDieWithoutFood()
        {
            var village = new Village();
            village.AddWorker("Alex", "miner");
            village.Food = 0;

            for (int i = 0; i < Worker.daysUntilStarvation + 1; i++)
            {
                village.Day();
            }

            Assert.False(village.Workers[0].Alive);
            Assert.True(village.Workers[0].DaysHungry == Worker.daysUntilStarvation);

        }

        [Fact]
        public void TestAddProject()
        {
            var village = new Village();
            village.Wood = 10;
            village.Metal = 10;

            village.AddProject("House");

            Assert.Single(village.Projects);
            Assert.Equal("House", village.Projects[0].Name);
        }

        [Fact]
        public void TestAddProjectWithoutEnoughResources()
        {
            var village = new Village();
            village.Wood = 1;
            village.Metal = 1;

            village.AddProject("House");

            Assert.Empty(village.Projects);

        }

        [Fact]
        public void TestCompleteProject()
        {
            var village = new Village();
            village.AddWorker("Anna", "builder");
            village.Wood = 10;
            village.Metal = 10;
            village.AddProject("House");

            while (village.Projects.Count > 0)
            {
                village.Day();
            }

            Assert.Empty(village.Projects);
            Assert.Equal(4, village.Buildings.Count);
            Assert.Contains(village.Buildings, building => building.Name == "House");

        }

        [Fact]
        public void TestCompleteGame()
        {
            var village = new Village();

            village.AddWorker("Nadir", "lumberjack");
            village.AddWorker("Samer", "miner");
            village.AddWorker("Rami", "farmer");

            for (int i = 0; i < 50; i++)
            {
                village.Day();
            }
            

            village.AddProject("Castle");

            village.AddWorker("Naser", "builder");

            for (int i = 0; i < 50; i++)
            { 
                village.Day();
            }

            Assert.Equal(4, village.Buildings.Count);
            Assert.Equal("Castle", village.Buildings[3].Name);

        }
    }


}  
    