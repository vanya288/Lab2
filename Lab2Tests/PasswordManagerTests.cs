using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using FakeItEasy;

namespace Lab2.Tests
{
    [TestClass()]
    public class PasswordManagerTests
    {
        PasswordManager manager;

        [TestInitialize]
        public void Initialize()
        {
            manager = new PasswordManager();
            //managerStub = new PasswordManagerStub();
        }

        [TestMethod()]
        [DataRow("", "z4PhNX7vuL3xVChQ1m2AB9Yg5AULVxXcg/SpIdNs6c5H0NE8XYXysP+DGNKHfuwvY7kxvUdBeoGlODJ6+SfaPg==")]
        [DataRow("password", "sQnzu7wkTrgkQZF+0G1hi5AI3Qmzvv0bXgc5THBqi7mAsdd4Xll27ASbRt9fEyavWi6m0QP9B8lThf+rDKy8hg==")]
        [DataRow("veryD1fficu1t__P#55w0r5*", "m1NX6GHuqyRfvNJmvZXfNL2UoMQcMK4mYk8vQbeEx/2DfpPOq3o82P8AgaQcz4jQC3ZuddFbQ8+NIujdwLHqRw==")]
        [DataRow("6\\Rn/\"Le7j'W*X6f", "rfWaWYnvEDBszeipX6d0OhSepkx0EAVIL5LSf9ZIt9Y6P1U2CdoYPhBpJKkB6kkcifDI7FUz+zbOEIHz1lXAOQ==")]
        public void CalculateSHA512_CalculatesSHA512_ForAnyText(String _secretText, String _expected)
        {
            Assert.AreEqual(_expected, manager.CalculateSHA512(_secretText));
        }

        [TestMethod()]
        [DataRow("", "", "uTbO6Gyfh6pdPG8uhMtaQjml/lBICm7Ga3CrWx9KxnMMbFFUIbMn7B1pQC5T37Sa1zgesGezOP17DLIiRyJdRw==")]
        [DataRow("password", "salt", "HI5DJGJkjYJa3kmD2kscnMIxGA090Od7DP4LKMXi8rOao62r/NXh/paLnoFQBc9nSZwwF39MAZnjkGTOqlre+g==")]
        [DataRow("veryD1fficu1t__P#55w0r5*", "Unkn0wn__5%1t", "qUY3szoexDyx63sIJJfKaZfQs9ZLv474bzucqhUDnV4wkb1Ke5oXygVjD1nHxP0/1KUjBiOBj459SmZWOF2Avg==")]
        [DataRow("e7j'W*X6f", "Lcx-enEfp2k,y[;", "U1osgpoj4UALEDP5wY5nv6pEdLqYBnLTA92iwQ8dvomurSVZ8+RE0+Pm16UAyH3Kmm8IfY1AdfR1NIcSK0Wwng==")]
        public void CalculateHMACSHA512_CalculatesHMACSHA512_ForAnyText(String _secretText, String _salt, String _expected)
        {
            Assert.AreEqual(_expected, manager.CalculateHMACSHA512(_secretText, _salt));
        }

        [TestMethod()]
        [DataRow("", "d41d8cd98f00b204e9800998ecf8427e")]
        [DataRow("password", "5f4dcc3b5aa765d61d8327deb882cf99")]
        [DataRow("veryD1fficu1t__P#55w0r5*", "1953ebd730f2535cadfa050e03c24300")]
        [DataRow("6\\Rn/\"Le7j'W*X6f", "5e235d857e5a05c0dd147b732a4cbb4e")]
        public void CalculateMD5_CalculatesMD5_ForAnyText(String _secretText, String _expected)
        {
            StringBuilder hash = new StringBuilder();
            byte[] bytes;

            bytes = manager.CalculateMD5(_secretText);

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            Assert.AreEqual(_expected, hash.ToString());
        }

        [TestMethod()]
        [DataRow(0)]
        [DataRow(-100)]
        public void GenerateSaltBytes_ThrowsArgumentOutOfRangeException_WhenParamIsLessThanOne(int _saltLength)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => manager.GenerateSaltBytes(_saltLength));
        }

        [TestMethod()]
        public void GenerateSaltBytes_Generates64BytesSalt_WhenNoParamsPassed()
        {
            Assert.IsTrue(manager.GenerateSaltBytes().Length == 64);
        }

        [TestMethod()]
        [DataRow(1)]
        [DataRow(50)]
        [DataRow(1000)]
        public void GenerateSaltBytes_GeneratesBytesSalt_WhenSaltLeghtPassed(int _expectedLength)
        {
            Assert.IsTrue(manager.GenerateSaltBytes(_expectedLength).Length == _expectedLength);
        }

        [TestMethod()]
        [DataRow("", new byte[] { 0x20, 0x20 })]
        [DataRow(null, new byte[] { 0x20, 0x20 })]
        public void EncryptAES_ThrowsArgumentNullException_WhenPasswordIsNullOrEmpty(String _pass, byte[] _key)
        {
            Assert.ThrowsException<ArgumentNullException>(() => manager.EncryptAES(_pass, _key));
        }

        [TestMethod()]
        [DataRow("pass", new byte[] { })]
        [DataRow("totalSecurity", null)]
        public void EncryptAES_ThrowsArgumentNullException_WhenKeyIsNullOrEmpty(String _pass, byte[] _key)
        {
            Assert.ThrowsException<ArgumentNullException>(() => manager.EncryptAES(_pass, _key));
        }

        [TestMethod()]
        [DataRow("", new byte[] { 0x20, 0x20 })]
        [DataRow(null, new byte[] { 0x20, 0x20 })]
        public void DecryptAES_ThrowsArgumentNullException_WhenPasswordIsNullOrEmpty(String _pass, byte[] _key)
        {
            Assert.ThrowsException<ArgumentNullException>(() => manager.DecryptAES(_pass, _key));
        }

        [TestMethod()]
        [DataRow("pass", new byte[] { })]
        [DataRow("totalSecurity", null)]
        public void DecryptAES_ThrowsArgumentNullException_WhenKeyIsNullOrEmpty(String _pass, byte[] _key)
        {
            Assert.ThrowsException<ArgumentNullException>(() => manager.DecryptAES(_pass, _key));
        }

        [TestMethod()]
        [DataRow("lhq/A+UFOr85dnsdENXupw==", "uTbO6Gyfh6pdPG8uhMtaQjml/lBICm7Ga3CrWx9KxnMMbFFUIbMn7B1pQC5T37Sa1zgesGezOP17DLIiRyJdRw==", "password")]
        [DataRow("vtVZ87BpJjELn9lC6/dHVgH/ZqaO0IDe3XzkfvmqzZs=", "uTbO6Gyfh6pdPG8uhMtaQjml/lBICm7Ga3CrWx9KxnMMbFFUIbMn7B1pQC5T37Sa1zgesGezOP17DLIiRyJdRw==", "veryD1fficu1t__P#55w0r5*")]
        [DataRow("BVf7/+X77sFsJsH+pfIrrg==", "uTbO6Gyfh6pdPG8uhMtaQjml/lBICm7Ga3CrWx9KxnMMbFFUIbMn7B1pQC5T37Sa1zgesGezOP17DLIiRyJdRw==", "6\\Rn/\"Le7j'W*X6f")]
        public void DecryptPasswordAES_DecryptsPassword_WhenPasswordIsValid(String _encrypted, String _key, String _expected)
        {
            Assert.AreEqual(_expected, manager.DecryptPasswordAES(_encrypted, _key));
        }

        [TestMethod()]
        [DataRow("password", "uPU+epTpghFJ6WUpRnOgxZQgPi8CFmVmTQ65XznBvZ63aojc6okgv7hd1Ntn1RRA9QFkIw7BAEYTdhZI0XUe8A==")]
        [DataRow("lhq/A+UFOr85dnsdENXupw==", "nM7QNEkaxzTbUVA3kuSrzYhnhnfebamIlkMsbz5+pkiySlVmGF60xvdjiNKmbk3Y5KHKiaT4DGjgXyDRAb3M7Q==")]
        [DataRow("veryD1fficu1t__P#55w0r5*", "jEpp1CvS/ytkPaKKz/jFjhbZb8O0vblHoNdZ9usjMvLD4d6NsA/l084wLlKWmBqF/4xWZXH0gik2cLzUFtHyAw==")]
        [DataRow("6\\Rn/\"Le7j'W*X6f", "+6eW4m6mq2eSSEOhsNKCB6uZ9YnmwuU4SXyo52dqkiWkwWr5JHZkI4X3OID5wh7+LcIms8m4ALJk0enJgPgPCg==")]
        public void CreatePasswordHashSHA512_CreatesSHA512Hash_ForAnyPassword(String _password, String _expected)
        {
            string fakeSalt = "6LAMgb986xeMZM5qfyq08xSEbeJUOm768y65AsAtFd+V5t/J8tf22RUE+EZ8HZkDFaMDM6KHDN+S713HnOGywA==";
            var managerStub = A.Fake<PasswordManager>();

            A.CallTo(() => managerStub.GenerateSaltString(64)).Returns(fakeSalt).Once();

            Assert.AreEqual(_expected, managerStub.CreatePasswordHashSHA512(_password));
        }

        [TestMethod()]
        [DataRow("password", "lnuD3qnCskmPRqCzok3yWKk2lOE3RPEZwnH5FqFQBJEg7yWtuf9l/SS6T/9i2wsP9yNAg9BpOZOVscIRQbVrDg==")]
        [DataRow("lhq/A+UFOr85dnsdENXupw==", "urtnPXiL0uy8DyoCtNER9R0bxM3dx7wZn49wYb3fX6vpC/fl59No5eN8KYy7JxGFGpM0OCoBUX+8YTbPBfqXOg==")]
        [DataRow("veryD1fficu1t__P#55w0r5*", "2UZRZSc1fpRgo7+WOSNKvnmnv3sAdqsJf7XrUI+A4Od5c1VVtDk0LR7Kz1iYXksctnO974GMWMm/SIftae7+Tw==")]
        [DataRow("6\\Rn/\"Le7j'W*X6f", "MJ5mX73tNBDBP2+rPJgTI0w14rPua/b19MInPNwyzMWUL+bb1z/wWoLwfYUfBMZnUf7JsqNrplQvkIx2FkX93A==")]
        public void CreatePasswordHashSHA512_CreatesHMACSHA512Hash_ForAnyPassword(String _password, String _expected)
        {
            string fakeSalt = "6LAMgb986xeMZM5qfyq08xSEbeJUOm768y65AsAtFd+V5t/J8tf22RUE+EZ8HZkDFaMDM6KHDN+S713HnOGywA==";
            var managerStub = A.Fake<PasswordManager>();

            A.CallTo(() => managerStub.GenerateSaltString(64)).Returns(fakeSalt).Once();

            Assert.AreEqual(_expected, managerStub.CreatePasswordHashHMACSHA512(_password));
        }
    }
}