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

            if (!context.Tag.Any())
            {
                // Add all the basic started tags, more tags are added as new games gets added. 
                AddTag(context, "Action");
                AddTag(context, "Adventure");
                AddTag(context, "Casual");
                AddTag(context, "Free to play");
                AddTag(context, "Strategy");
                AddTag(context, "SinglePlayer");
            }

            if (!context.Game.Any())
            {
                Addgame(context,"Warframe","Digital Extremes","Action,Free to play");
                Addgame(context, "Minecraft", "Notch", "Survival,Creative");
                Addgame(context, "Overwatch", "Blizzard", "FPS,Action");
                Addgame(context, "Warcraft", "Blizzard", "Fantasy,MMO,Free to play");
                Addgame(context, "Call of duty", "Activision", "Action,FPS");
                Addgame(context, "HITMAN 3", "IO Interactive", "Stealth");
                Addgame(context, "Grand Theft Auto", "Rockstar Games", "Action");
                Addgame(context, "Monster Hunter World", "Capcom", "Action, RPG");
                Addgame(context, "Final Fantasy ", "Capcom", "Action, RPG, Fantasy");
            }
        }
        public static void AddTag(LibraryContext context, string tagName_)
        {
            var newTag = new Tag()
            {
                TagName = tagName_
            };
            context.Tag.Add(newTag);
            context.SaveChanges();
        }

        public static void Addgame(LibraryContext context, string _title,string _dev, string _tags,GameScores _gamescore = null)
        {
            //Check if the game already exists
            IQueryable<Game> gamequery = context.Game;
            gamequery = gamequery.Where(d => d.Title == _title);
            if (!gamequery.Any())
            {
                //If no scores put in put a 0 score in
                if (_gamescore == null)
                {
                    _gamescore = new GameScores();
                    _gamescore.GeneralScore = 0;
                    _gamescore.Fun = 0;
                    _gamescore.Replayability = 0;
                    _gamescore.Action = 0;
                }
                context.GameScores.Add(_gamescore);
                context.SaveChanges();

                //Find dev
                IQueryable<Developer> devsquery = context.Developers;
                devsquery = devsquery.Where(d => d.DevName == _dev);
                Developer linkedDev;
                if (!devsquery.Any())
                {
                    linkedDev = new Developer();
                    linkedDev.DevName = _dev;
                    context.Developers.Add(linkedDev);
                    context.SaveChanges();

                }
                else
                {
                    linkedDev = devsquery.First();
                }


                Game newgame = new Game()
                {
                    Title = _title,
                    GameScores = _gamescore,
                    Developer = linkedDev
                };

                context.Game.Add(newgame);
                context.SaveChanges();

                //Connect relevant tags
                string[] tagsArr = _tags.Split(",");
                foreach (string i in tagsArr)
                {
                    //Find tag
                    IQueryable<Tag> tagsquery = context.Tag;
                    tagsquery = tagsquery.Where(d => d.TagName == i);
                    Tag linkedTag;
                    if (!tagsquery.Any())
                    {
                        linkedTag = new Tag();
                        linkedTag.TagName = i;
                        context.Tag.Add(linkedTag);
                        context.SaveChanges();

                    }
                    else
                    {
                        linkedTag = tagsquery.First();
                    }

                    TagsLink newtagslink = new TagsLink();
                    newtagslink.Game = newgame;
                    newtagslink.Tag = linkedTag;

                    context.TagLink.Add(newtagslink);
                    context.SaveChanges();
                }
            }
            else
            {
            }


        }
    }
}
