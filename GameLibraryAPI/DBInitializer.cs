using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class DBInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Game.Any())
            {
                var newGameScores = new GameScores()
                {
                    GeneralScore = 9,
                    Fun = 7,
                    Replayability = 7,
                    Action = 8
                };
                context.GameScores.Add(newGameScores);
                context.SaveChanges();

                // Only add the best game to start with
                var newGame = new Game()
                {
                    Title = "WarFrame",
                    Developers = "Digital Extremes",
                    GameScores = newGameScores
                };
                context.Game.Add(newGame);
                context.SaveChanges();
                newGame = new Game()
                {
                    Title = "Minecraft",
                    Developers = "Microsoft",
                    GameScores = newGameScores
                };
                context.Game.Add(newGame);
                context.SaveChanges();
                newGame = new Game()
                {
                    Title = "Overwatch",
                    Developers = "Blizzard",
                    GameScores = newGameScores
                };
                context.Game.Add(newGame);
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                // Add all the basic started tags, more tags can be added in the future :D
                AddTag(context, "Action");
                AddTag(context, "Adventure");
                AddTag(context, "Casual");
                AddTag(context, "Free to play");
                AddTag(context, "Strategy");
                AddTag(context, "SinglePlayer");
            }

            if (!context.TagsLink.Any())
            {
                context.SaveChanges();
            }
            if (!context.GameScores.Any())
            {
                context.SaveChanges();
            }
            if (!context.PlayerScore.Any())
            {
                context.SaveChanges();
            }

        }
        public static void AddTag(LibraryContext context, string tagName_)
        {
            var newTag = new Tags()
            {
                TagName = tagName_
            };
            context.Tags.Add(newTag);
            context.SaveChanges();
        }
    }
}
