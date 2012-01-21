/*
 * 
 * Welcome to this tutorial.  Things you should know from a previous guide: how to work in Visual Studio or 
 * C# Express, how to code in general (general practices or naming conventions, program flow, creating 
 * classes/methods, etc.
 * 
 * Level: Beginner
 * Purpose: To get the user familiar with commands and how they are integrateable with TShock.
 * 
 * 
 */

using System;
using TShockAPI;
using Terraria;

namespace Bunnies
{
    /* This attribute is read by the server.  When it loads plugins, it only loads plugins with the same API Version.
     * When updating plugins, this needs to be changed, and often times, is the only thing that needs to change.
     */
    [APIVersion(1, 11)]

    /*This is your main class.  This is what gets constructed immediately after being loaded by the server.
     * What happens after that is dependant on order.
     * 
     * First off, you want to make sure you "extend" the TerrariaPlugin class.  This is achieved by attaching 
     * the suffix ": TerrariaPlugin" to the end of a class.  Please read up on class hierarchy for a more detailed
     * explaination of extends.
     */
    public class Bunnies : TerrariaPlugin
    {
        /* Override this method of the "base" class.  Overriding a method means instead of using TerrariaPlugin's
         * default method, it will use this one, which you can customize to fit your liking. 
         * This tells the server what version of the plugin is running.
         * This can be useful for figuring out why some people are experiencing issues as it tells you what version
         * they were using.
         */
        public override Version Version
        {
            /*Some people read this directly from an assemblyinfo.cs file, but this is simpler.
             */
            get { return new Version("Final"); }
        }

        /* Again, override this so that servers can tell what the plugin's name is.  Only useful for
         * logs.
         */
        public override string Name
        {
            get { return "BUNNIES"; }
        }

        /* This tells the plugin who wrote the plugin.
         */
        public override string Author
        {
            get { return "Bunny Lovers"; }
        }

        /* Not sure this is even used, but its your plugin description.  This is good to do since it will
         * tell all users and people interested in your code what it does.
         */
        public override string Description
        {
            get { return "Everyone wants bunnies!"; }
        }

        /* The constructor for your main class.  Make sure it has an argument of Main ( you can call it whatever you want )
         * And make sure you pass it to the parent using " : base()"
         */
        public Bunnies(Main game)
            : base(game)
        {
            /*TShock by default runs at order 0.  If you wish to load before tshock ( for overriding tshock handlers )
             * set Order to a negative number.  If you want to wait til tshock has finished loading, set Order > 0
             */
            Order = 4;
        }

        /* This must be implemented in every plugin.  However, it is up to you whether you want to put code here.
         * This is called by the server after your plugin has been initialized ( see order above ).
         */
        public override void Initialize()
        {
            /* Now the actual tutorial bit.  Adding commands to TShock is extremely useful.  It allows you to hook onto
             * the very convienant and extremely powerful command handling system.  It automatically calls your method 
             * when the command is executed and creates nice variables stored in a CommandArgs type.
             * 
             * First off, you want to call TShockAPI.Commands, then you want to get that objects member ChatCommands.
             * Then you want to add a new Command to it.  A command takes the form 
             *          new Command( "perm", MethodToBeCalled, "commandingame" )
             * "perm" is the permission in the group db that corresponds to your command.  Leaving this blank/empty or
             * null allows everyone to use your command.  You can use TShockAPI.Permissions.XXX or you can use strings.
             * MethodToBeCalled is the method you want to execute when this command is used.  It must have the argument
             * COmmandArgs and no return type.
             * "commandingame" is the command they would execute in game without the "/".
             * 
             * Easy right?
             */
            Commands.ChatCommands.Add(new Command("", Bunny, "bunny"));
        }

        /* This can be private, public, static, etc.
         * Notice that the method name is the same as above.
         * 
         * CommandArgs have two useful things for most operations
         * args.Player returns the player who executed the command, so you can send messages back to them,
         * kick them, ban them, do whatever you want to them.
         * args.Parameters is a list of the parameters used in game.  Things quoted in quotes are one parameter.
         * 
         * More advanced users may find having the Player object that terraria uses useful.  That is also included
         * in CommandArgs.
         */
        private void Bunny(CommandArgs args)
        {
            /* Not sure why we do this, force of habbit.
             * Check to make sure the player isn't null before we try to operate on it.
             */
            if( args.Player != null )
            {
                /* This method will set a specific buff on a player for a specified time.
                 * 40 happens to be the buff for bunnies, and the complete list can be found
                 * on the wiki.
                 * 3600 refers to 3600 seconds.  Or 6 minutes.
                 * the boolean was added recently, and I am not sure what it's purpose is.
                 * Use true unless told otherwise.
                 */
                args.Player.SetBuff( 40, 3600, true);
            }
        }
    }
}
