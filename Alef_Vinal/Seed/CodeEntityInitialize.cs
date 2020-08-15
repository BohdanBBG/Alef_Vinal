using Alef_Vinal.Models;
using Alef_Vinal.Repositories;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Seed
{
    public class CodeEntityInitialize
    {
        public static async Task InitializeAsync(IDataRepository db, int count = 10)
        {
            Faker faker = new Faker();

            for (int i = 0; i < count; i++)
            {
                int threeDigitNumberInt = faker.Random.UShort(0, 999);
                string threeDigitNumberString = "";
                
                if(threeDigitNumberInt.ToString().Length == 1)
                {
                    threeDigitNumberString = "00" + threeDigitNumberInt.ToString();
                }

                if (threeDigitNumberInt.ToString().Length == 2)
                {
                    threeDigitNumberString = "0" + threeDigitNumberInt.ToString();
                }

                if (threeDigitNumberInt.ToString().Length == 3)
                {
                    threeDigitNumberString =  threeDigitNumberInt.ToString();
                }


                await db.Add(new CodeEntity
                {
                    Name = faker.Lorem.Word(),
                    Value = threeDigitNumberString
                });
            }

        }
    }
}
