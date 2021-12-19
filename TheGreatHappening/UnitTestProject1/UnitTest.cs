
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TheConference_UWP;
using TheConference_UWP.Services;
using System.Linq;

namespace UnitTestProject1
{
    /// <summary>
    /// Test of DeleteParticipantFromList, ChooseDiscountMessage and SaveParticipantToList 
    /// </summary>

    [TestClass]
    public class UwpParticipantShould
    {
        [TestMethod]
        
        public void DeleteParticipantFromList_DeletesParticipantFromList()
        {
            // Arrange
            List<UwpParticipant> list = new List<UwpParticipant>();
            var sut_ulf = new UwpParticipant("Ulf", "Lassgård", "ulf@varg.ab", "nej");
            var sut_sven = new UwpParticipant("Sven", "Markos", "markolio@this", "nej");
            list.Add(sut_ulf);
            list.Add(sut_sven);
            var listCountToCheck = list.Count();

            // Action
            var updatedList = UwpParticipant.DeleteParticipantFromList(list, sut_ulf);

            // Assert
            Assert.AreNotEqual(listCountToCheck, updatedList.Count);
            Assert.IsTrue(updatedList.Count <= 1);
            Assert.AreEqual(sut_sven, updatedList[0]);
            Assert.AreNotEqual(sut_ulf, updatedList[0]);
        }

        [TestMethod]
        public void ChooseDiscountMessage_IfFullNameLastCharacterIsNotSAddS()
        {
            // Arrange
            var sara = new UwpParticipant("Sara", "Ås", null, null);
            var mia = new UwpParticipant("Mia", "Björn", null, null);

            // Assert
            Assert.AreEqual($"Sara Ås rabattkod är {sara.DiscountCode}.", UwpParticipant.ChooseDiscountMessage(sara));
            Assert.AreEqual($"Mia Björns rabattkod är {mia.DiscountCode}.", UwpParticipant.ChooseDiscountMessage(mia));
        }

        [TestMethod]
        public void SaveParticipantToList_ReturnsListOfParticipants()    
        {
            // Arrange              
            List<UwpParticipant> list = new List<UwpParticipant>();
            var ulf = new UwpParticipant("Ulf", "Lassgård", "ulf@varg.ab", "nej");
            var max = new UwpParticipant("Max", "Lax", "flax@många.lax", "nej");

            //Action
            UwpParticipant.SaveParticipantToList(list, ulf);
            UwpParticipant.SaveParticipantToList(list, max);

            //Assert
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(ulf, list[0]);
            Assert.AreEqual(max, list[1]);
        }
    }
}




