using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.DatabaseLogic.Repositories.Safety;
using Database.Core.Interfaces;
using Database.Entities.Safety;
using NUnit.Framework;
using Rhino.Mocks;

namespace Project.Tests.DbRepositories.Safety
{
    [TestFixture]
    public class TypesOfSafetyPointsRepositoryTest
    {
        private ITypesOfSafetyPointsRepository m_typesOfSafetyPointsRepository;

        [SetUp]
        public void Init()
        {
            m_typesOfSafetyPointsRepository = new TypesOfSafetyPointsRepository()
                {
                       DataBaseContext = zmokuj_DataBaseContext_ze_sztucznymi_rolami()
                };
        }

      

        [Test]
        public void GetAllTypesList__zwraca_liste_wszystkich_typow()
        {
            //
            IEnumerable<TypeOfSafetyPoint> result = m_typesOfSafetyPointsRepository.GetAll();
            //
            Assert.IsNotNull(result, "nie znaleziono typow");
            Assert.AreEqual(result.Count(), TypeOfSafetyPointList().Count, "nie znaleziono wszystkich typow");
        }

        [Test]
        public void GetOneTypes_dla_istniejacego_typu__zwraca_znaleziony_obiekt()
        {
            //
            TypeOfSafetyPoint result = m_typesOfSafetyPointsRepository.Get(1);
            //
            Assert.IsNotNull(result, "nie znaleziono typow");
            Assert.AreEqual(result.Name, TypeOfSafetyPointList()[0].Name, "znaleziono zly obiekt");
        }

        [Test]
        public void GetOneType_dla_nieistniejacego_typu__zwraca_null()
        {
            //
            TypeOfSafetyPoint result = m_typesOfSafetyPointsRepository.Get(-1);
            //
            Assert.IsNull(result, "znaleziono typ");
        }


        private List<TypeOfSafetyPoint> TypeOfSafetyPointList()
        {
            return new List<TypeOfSafetyPoint> {
                new TypeOfSafetyPoint { IdTypeOfSafetyPoint = 1,Name = "Warehouse"},
                new TypeOfSafetyPoint { IdTypeOfSafetyPoint = 2,Name = "ServicesWarehouse"},
                new TypeOfSafetyPoint { IdTypeOfSafetyPoint = 3,Name = "Products"}
            };
        }

        private IDataBaseContext zmokuj_DataBaseContext_ze_sztucznymi_rolami()
        {
            var dataBaseContext = MockRepository.GenerateStub<IDataBaseContext>();
            
            dataBaseContext.TypesOfSafetyPoints = new FakeDbSet<TypeOfSafetyPoint>();
            foreach (TypeOfSafetyPoint typeOfSafetyPoint in TypeOfSafetyPointList())
                dataBaseContext.TypesOfSafetyPoints.Add(typeOfSafetyPoint);
            return dataBaseContext;
        }

    }
}