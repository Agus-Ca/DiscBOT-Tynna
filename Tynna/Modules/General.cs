using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tynna.Modules
{
    public class General : ModuleBase
    {
        [Command("help")]
        public async Task Help()
        {
            Console.WriteLine($"\nEl usuario {Context.User.Username} ha usado el comando -HELP-");

            System.String commandPing = "No hago nada, soy el primer comando. Totalmente al pedo, borrenme porfa!";
            System.String commandInfo = "Te doy una info uwu";

            var builder = new EmbedBuilder()
                .WithDescription("Comandos:")
                .WithColor(new Color(247, 220, 111))
                .AddField("!ping", commandPing, false)
                .AddField("!info", commandInfo, false)
                .WithCurrentTimestamp();
            var embed = builder.Build();

            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("ping")]
        public async Task Ping()
        {
            Console.WriteLine($"\nEl usuario {Context.User.Username} ha usado el comando -PING-");

            await Context.Channel.SendMessageAsync("pong!");
        }

        [Command("info")]
        public async Task Info()
        {
            Console.WriteLine($"\nEl usuario {Context.User.Username} ha usado el comando -INFO-");

            var userId = Context.User.Id;
            var userDiscriminator = Context.User.Discriminator;
            var userCreatedAt = Context.User.CreatedAt;
            var userJoinedAt = (Context.User as SocketGuildUser).JoinedAt.Value.ToString("dd/MM/yyyy");
            var userRoles = string.Join(" && ", (Context.User as SocketGuildUser).Roles);
            string description = Context.User.Username switch
            {
                "Yami" => $"{Context.User.Username}, la chica mas bonita que el admin conoce :smiling_face_with_3_hearts:",
                "AgusCa97" => $"{Context.User.Username}, un chico copado",
                _ => "404 not found. Ups!",
            };

            var builder = new EmbedBuilder()
                .WithThumbnailUrl((Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl()))
                .WithDescription("Información acerca de ti:")
                .WithColor(new Color(108, 52, 131))
                .AddField("ID del usuario", userId, true)
                .AddField("Discriminador", userDiscriminator, true)
                .AddField("Fecha creación", userCreatedAt.ToString("dd/MM/yyyy"), false)
                .AddField("Se unió", userJoinedAt, true)
                .AddField("Roles", userRoles, false)
                .AddField("Descripción", description, false)
                .WithCurrentTimestamp();
            var embed = builder.Build();

            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}