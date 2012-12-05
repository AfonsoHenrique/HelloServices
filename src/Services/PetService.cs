//
// PetService.cs
//
// Author:
//       Tony Alexander Hild <tony_hild@yahoo.com>
//
// Copyright (p) 2012 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Funq;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.ServiceInterface;

namespace HelloServices
{
    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class PetService : RestServiceBase<Pet>
    {
        public override object OnGet(Pet pet)
        {
            if (pet.Id == Guid.Empty)
            {
                return from n in PetDatabase.Instace.Pets
                       select n;
            }
            return (from n in PetDatabase.Instace.Pets
                    where n.Id == pet.Id
                    select n).SingleOrDefault();
        }

        public override object OnDelete(Pet pet)
        {
            if (pet.Id == Guid.Empty)
            {
                foreach (Pet p in PetDatabase.Instace.Pets)
                {
                    PetDatabase.Instace.Pets.Remove(p);
                }
            }
            else
            {
                foreach (Pet p in PetDatabase.Instace.Pets)
                {
                    if (p.Id == pet.Id)
                    {
                        PetDatabase.Instace.Pets.Remove(p);
                    }
                }
            }

            return base.OnDelete(pet);
        }
    }

    [Route("/v1.0/dogs")]
    [Route("/v1.0/dogs/{Id}")]
    public class Dog : Pet
    {
    }

    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class DogService : RestServiceBase<Dog>
    {
        public override object OnGet(Dog dog)
        {
            if (dog.Id == Guid.Empty)
            {
                return from n in PetDatabase.Instace.Pets
                       where n.GetType() == typeof(Dog)
                       select n;
            }
            return (from n in PetDatabase.Instace.Pets
                    where n.Id == dog.Id
                    select n).SingleOrDefault();
        }
    }

    [Route("/v2.0/dogs")]
    [Route("/v2.0/dogs/{Id}")]
    public class DogNewService : Dog
    {
    }

    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class DogServiceNew : RestServiceBase<DogNewService>
    {
        public override object OnGet(DogNewService dog)
        {
            if (dog.Id == Guid.Empty)
            {
                return from n in PetDatabase.Instace.Pets
                       where n.GetType() == typeof(Dog)
                       select n;
            }
            return (from n in PetDatabase.Instace.Pets
                    where n.Id == dog.Id
                    select n).SingleOrDefault();
        }

        public override object OnPut(DogNewService dog)
        {
            DogNewService q = (from n in PetDatabase.Instace.Pets where n.Id == dog.Id select n).FirstOrDefault() as DogNewService;

            q.Name = dog.Name;
            return q;
        }

        public override object OnPost(DogNewService dog)
        {
            PetDatabase.Instace.Pets.Add(dog);
            return dog;
        }

        public override object OnDelete(DogNewService dog)
        {
            PetDatabase.Instace.Pets.Remove(dog);

            return dog;
        }
    }

    [Route("/v2.0/cats")]
    [Route("/v2.0/cats/{Id}")]
    public class Cat : Pet
    {
    }

    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class CatService : RestServiceBase<Cat>
    {
        public override object OnGet(Cat cat)
        {
            if (cat.Id == Guid.Empty)
            {
                return from n in PetDatabase.Instace.Pets
                       where n.GetType() == typeof(Cat)
                       select n;
            }
            return (from n in PetDatabase.Instace.Pets
                    where n.Id == cat.Id
                    select n).SingleOrDefault();
        }
        public override object OnPut(Cat cat)
        {
            Cat q = (from n in PetDatabase.Instace.Pets where n.Id == cat.Id select n).FirstOrDefault() as Cat;

            q.Name = cat.Name;
            return q;
        }

        public override object OnPost(Cat cat)
        {
            PetDatabase.Instace.Pets.Add(cat);
            return cat;
        }

        public override object OnDelete(Cat cat)
        {
            PetDatabase.Instace.Pets.Remove(cat);

            return cat;
        }
    }

    [Route("/v2.0/parrots")]
    [Route("/v2.0/parrots/{Id}")]
    public class Parrot : Pet
    {
    }

    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class ParrotService : RestServiceBase<Parrot>
    {
        public override object OnGet(Parrot parrot)
        {
            if (parrot.Id == Guid.Empty)
            {
                return from n in PetDatabase.Instace.Pets
                       where n.GetType() == typeof(Parrot)
                       select n;
            }
            return (from n in PetDatabase.Instace.Pets
                    where n.Id == parrot.Id
                    select n).SingleOrDefault();
        }

        public override object OnPut(Parrot parrot)
        {
            Parrot q = (from n in PetDatabase.Instace.Pets where n.Id == parrot.Id select n).FirstOrDefault() as Parrot;

            q.Name = parrot.Name;
            return q;
        }

        public override object OnPost(Parrot parrot)
        {
            PetDatabase.Instace.Pets.Add(parrot);
            return parrot;
        }

        public override object OnDelete(Parrot parrot)
        {
            PetDatabase.Instace.Pets.Remove(parrot);

            return parrot;
        }
    }


    /// <summary>
    /// Define your ServiceStack web service parrot (i.e. the Request DTO).
    /// </summary>
    [Route("/pets/type/{PetType}")]
    public class Pets
    {
        public string PetType { get; set; }
    }

    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class PetsService : RestServiceBase<Pets>
    {
        public override object OnGet(Pets pets)
        {
            //            if (string.IsNullOrEmpty (pets.PetType)) {
            //                return from n in PetDatabase.Instace.Pets 
            //                    select n;
            //            }
            return from n in PetDatabase.Instace.Pets
                   where n.GetType().Name.ToLower() == pets.PetType.ToLower()
                   select n;
        }
    }

    public class PetDatabase
    {

        static PetDatabase _instance;
        static object _lock = new object();
        IList<Pet> _pets = new List<Pet>();

        public Pet this[int index]
        {
            get { return _pets[index]; }
        }

        public IList<Pet> Pets
        {
            get { return _pets; }
        }

        public static PetDatabase Instace
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = new PetDatabase();
                        _instance._pets.Add(new Dog() { Name = "Spike", Id = Guid.NewGuid() });
                        _instance._pets.Add(new Dog() { Name = "Bo", Id = Guid.NewGuid() });
                        _instance._pets.Add(new Dog() { Name = "Mike", Id = Guid.NewGuid() });
                        _instance._pets.Add(new Cat() { Name = "Kitty", Id = Guid.NewGuid() });
                        _instance._pets.Add(new Cat() { Name = "Mimi", Id = Guid.NewGuid() });
                        _instance._pets.Add(new Cat() { Name = "FurBall", Id = Guid.NewGuid() });
                        _instance._pets.Add(new Parrot() { Name = "Polly", Id = Guid.NewGuid() });
                        _instance._pets.Add(new Parrot() { Name = "Rico", Id = Guid.NewGuid() });
                    }
                }
                return _instance;
            }
        }

    }

}

