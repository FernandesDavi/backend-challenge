using System.Collections.Generic;
using AutoFixture;
using Domain.CommandHandler;
using Domain.Commands;
using Domain.Entities;
using Domain.Notifications;
using Domain.Repositories;
using NSubstitute;
using Tests.Customization;
using Xunit;

namespace Tests.Core.Domain.CommandHandler
{
    public class AtualizarPedidoCommandHandlerTest
    {

        public IFixture _fixture { get; set; }

        public AtualizarPedidoCommandHandlerTest()
        {
            this._fixture = new Fixture().Customize(new AutoPopulatedNSubstitutePropertiesCustomization());

        }
        [Fact]
        public async void AtualizarPedidoComSucesso()
        {
            var PedidoRepository = Substitute.For<IPedidoCommandRepository>();
            var command = new PedidoCommand("123", new List<PedidoItens>{
                new PedidoItens{
                    Descricao ="qwe",
                    PrecoUnitario=12,
                    Quantidade= 2
                }
            });

            PedidoRepository.AtualizarPedido(command);
            var commandHandler = new AtualizarPedidoCommandHandler(new NotificationPool(), PedidoRepository);
            await commandHandler.Handle(command);
            Assert.False(commandHandler.HasNotifications);
        }
    }
}