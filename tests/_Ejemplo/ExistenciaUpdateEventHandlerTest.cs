using _Ejemplo.Config;
using Inventario.Application.Commands;
using Inventario.Application.Handlers;
using Inventario.Entities;
using Inventario.Entities.DbContexts;
using Inventario.Shared.Common;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _Ejemplo
{
    [TestClass]
    public class ExistenciaUpdateEventHandlerTest
    {
        private ILogger<ExistenciaUpdateEventHandler> GetLogger
        {
            get { return new Mock<ILogger<ExistenciaUpdateEventHandler>>().Object; }
        }

        //private readonly RepositoryContextFactory _context;

        //public ExistenciaUpdateEventHandlerTest(RepositoryContextFactory context)
        //{
        //    _context = context;
        //}

        [TestMethod]
        public void TryDisminuirStockCuandoArticuloTieneStock ()
        {
            var context = RepositoryContextFactoryInMemory.Get();
            //var context = _context;

            var existenciaId = 1;
            var articuloId = 1;

            context.Existencias.Add(new Existencia
            {
                ExistenciaId = existenciaId,
                ArticuloId = articuloId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ExistenciaUpdateEventHandler(context, GetLogger);

            handler.Handle(new ExistenciaUpdateCommand
            {
                Items = new List<ExistenciaUpdateItem>()
                {
                    new ExistenciaUpdateItem
                    {
                        ArticuloId=articuloId,
                        Stock=0,
                        Action = Enums.ExistenciaAction.Substract
                    }
                    
                }
            }, new CancellationToken()).Wait();

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TryDisminuirStockCuandoArticuloNoTieneStock()
        {
            var context = RepositoryContextFactoryInMemory.Get();
            //var context = _context;

            var existenciaId = 2;
            var articuloId = 1;

            context.Existencias.Add(new Existencia
            {
                ExistenciaId = existenciaId,
                ArticuloId = articuloId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ExistenciaUpdateEventHandler(context, GetLogger);

            try
            {
                handler.Handle(new ExistenciaUpdateCommand
                {
                    Items = new List<ExistenciaUpdateItem>()
                {
                    new ExistenciaUpdateItem
                    {
                        ArticuloId=articuloId,
                        Stock=10,
                        Action = Enums.ExistenciaAction.Substract
                    }

                }
                }, new CancellationToken()).Wait();

            }
            catch (Exception ae)
            {
                var excepction = ae;
                throw;
            }      

        }

        [TestMethod]
        public void TryAumentarStockCuandoArticuloExiste()
        {
            var context = RepositoryContextFactoryInMemory.Get();
            //var context = _context;

            var existenciaId = 3;
            var articuloId = 3;

            context.Existencias.Add(new Existencia
            {
                ExistenciaId = existenciaId,
                ArticuloId = articuloId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ExistenciaUpdateEventHandler(context, GetLogger);

            try
            {
                handler.Handle(new ExistenciaUpdateCommand
                {
                    Items = new List<ExistenciaUpdateItem>()
                {
                    new ExistenciaUpdateItem
                    {
                        ArticuloId=articuloId,
                        Stock=2,
                        Action = Enums.ExistenciaAction.Add
                    }

                }
                }, new CancellationToken()).Wait();

                var stockDb = context.Existencias.Single(x=>x.ArticuloId == articuloId).Stock;
                Assert.AreEqual(stockDb,3);

            }
            catch (Exception ae)
            {
                var excepction = ae;
                throw;
            }

        }

        [TestMethod]
        public void TryAumentarStockCuandoArticuloNoExiste()
        {
            var context = RepositoryContextFactoryInMemory.Get();
            //var context = _context;

            var existenciaId = 3;
            var articuloId = 3;

            context.Existencias.Add(new Existencia
            {
                ExistenciaId = existenciaId,               
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ExistenciaUpdateEventHandler(context, GetLogger);

            try
            {
                handler.Handle(new ExistenciaUpdateCommand
                {
                    Items = new List<ExistenciaUpdateItem>()
                {
                    new ExistenciaUpdateItem
                    {
                        ArticuloId=articuloId,
                        Stock=2,
                        Action = Enums.ExistenciaAction.Add
                    }

                }
                }, new CancellationToken()).Wait();

                var stockDb = context.Existencias.Single(x => x.ArticuloId == articuloId).Stock;
                Assert.AreEqual(stockDb, 2);

            }
            catch (Exception ae)
            {
                var excepction = ae;
                throw;
            }

        }
    }
}