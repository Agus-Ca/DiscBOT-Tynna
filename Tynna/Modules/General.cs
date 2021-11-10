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
            System.String commandInfo = "Te doy una info tuya o de alguien (si lo mencionas) uwu";
            System.String commandPurge = "Te borro los últimos X mensages -.-";
            System.String commandServer = "Te una info pero del server owo";

            var builder = new EmbedBuilder()
                .WithDescription("Comandos:")
                .WithColor(new Color(247, 220, 111))
                .AddField("!ping", commandPing, false)
                .AddField("!info", commandInfo, false)
                .AddField("!purge", commandPurge, false)
                .AddField("!server", commandServer, false)
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
        public async Task Info(SocketGuildUser user = null)
        {
            Console.WriteLine($"\nEl usuario {Context.User.Username} ha usado el comando -INFO-");

            user ??= (SocketGuildUser)Context.User;

            System.String userAvatarUrl = user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl();
            System.UInt64 userId = user.Id;
            System.String userDiscriminator = user.Discriminator;
            System.String userCreatedAt = user.CreatedAt.ToString("dd/MM/yyyy");
            System.String userJoinedAt = user.JoinedAt.Value.ToString("dd/MM/yyyy");
            System.String userRoles = string.Join(" && ", user.Roles.Select(x => x.Mention));
            string description = user.Username switch
                {
                    "Yami" => $"{user.Username}, la chica mas bonita que el admin conoce :smiling_face_with_3_hearts:",
                    "AgusCa97" => $"{user.Username}, un chico copado",
                    _ => "404 not found. Ups!",
                };      

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(userAvatarUrl)
                .WithDescription("Información acerca de ti:")
                .WithColor(new Color(108, 52, 131))
                .AddField("ID del usuario", userId, true)
                .AddField("Discriminador", userDiscriminator, true)
                .AddField("Fecha creación", userCreatedAt, false)
                .AddField("Se unió", userJoinedAt, true)
                .AddField("Roles", userRoles, false)
                .AddField("Descripción", description, false)
                .WithCurrentTimestamp();
            var embed = builder.Build();

            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("purge")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Purge(int amount)
        {
            Console.WriteLine($"\nEl usuario {Context.User.Username} ha usado el comando -PURGE-");

            var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

            var message = await Context.Channel.SendMessageAsync($"{messages.Count()} mensajes eliminados correctamente!");
            await Task.Delay(2000);
            await message.DeleteAsync();
        }

        [Command("server")]
        public async Task Server()
        {
            Console.WriteLine($"\nEl usuario {Context.User.Username} ha usado el comando -SERVER-");

            var createdAt = Context.Guild.CreatedAt.ToString("dd/MM/yyyy");
            var memberCount = (Context.Guild as SocketGuild).MemberCount + " miembros";
            var onlineMembersCount = (Context.Guild as SocketGuild).Users.Where(x => x.Status == UserStatus.Online).Count() + " members";
            var offlineMembersCount = (Context.Guild as SocketGuild).Users.Where(x => x.Status == UserStatus.Offline).Count() + " members";
            var idleMembersCount = (Context.Guild as SocketGuild).Users.Where(x => x.Status == UserStatus.Idle).Count() + " members";
            var dndMembersCount = (Context.Guild as SocketGuild).Users.Where(x => x.Status == UserStatus.DoNotDisturb).Count() + " members";
            var afkMembersCount = (Context.Guild as SocketGuild).Users.Where(x => x.Status == UserStatus.AFK).Count() + " members";
            var invisibleMembersCount = (Context.Guild as SocketGuild).Users.Where(x => x.Status == UserStatus.Invisible).Count() + " members";

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithDescription("Información acerca del servidor:")
                .WithTitle($"{Context.Guild.Name} información")
                .WithColor(new Color(108, 52, 131))
                .AddField("Creado el", createdAt, false)
                .AddField("Cantidad de miembros", memberCount, false)
                .AddField("Cantidad de miembros online", onlineMembersCount, true)
                .AddField("Cantidad de miembros offline", offlineMembersCount, true)
                .AddField("Cantidad de miembros inactivos", idleMembersCount, true)
                .AddField("Cantidad de miembros no molestar", dndMembersCount, true)
                .AddField("Cantidad de miembros afk", afkMembersCount, true)
                .AddField("Cantidad de miembros invisibles", invisibleMembersCount, true)
                .WithCurrentTimestamp();
            var embed = builder.Build();

            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}