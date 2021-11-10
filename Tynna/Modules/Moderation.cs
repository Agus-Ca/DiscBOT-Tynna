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
    public class Moderation : ModuleBase
    {
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
    }
}
