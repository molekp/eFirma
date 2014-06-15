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
    public class SafetyPointsRepositoryTest
    {
        private ISafetyPointRepository m_safetyPointsRepository;

        [SetUp]
        public void Init()
        {
            m_safetyPointsRepository = new SafetyPointRepository()
                {
                       DataBaseContext = zmokuj_DataBaseContext_ze_sztucznymi_SafetyPoints()
                };
        }


        [Test]
        public void CreateSafetyPoint_dla_poprawnego_SafetyPointa__zwraca_prawde()
        {
            var safetyPoint = new SafetyPoint();
            //
            bool result = m_safetyPointsRepository.CreateSafetyPoint(safetyPoint);
            //
            Assert.IsTrue(result);
        }

        [Test]
        public void GetAll__zwraca_liste_SafetyPointow()
        {
            //
            var result = m_safetyPointsRepository.GetAll();
            //
            Assert.IsNotNull(result);
            Assert.AreEqual(2,result.Count());
        }


        private List<SafetyPoint> SafetyPointList()
        {
            return new List<SafetyPoint>
                {
                        new SafetyPoint (),
                        new SafetyPoint()
                };
        }

        private IDataBaseContext zmokuj_DataBaseContext_ze_sztucznymi_SafetyPoints()
        {
            var dataBaseContext = MockRepository.GenerateStub<IDataBaseContext>();
            
            dataBaseContext.SafetyPoints = new FakeDbSet<SafetyPoint>();
            foreach (SafetyPoint safetyPoint in SafetyPointList())
                dataBaseContext.SafetyPoints.Add(safetyPoint);
            return dataBaseContext;
        }

    }
}