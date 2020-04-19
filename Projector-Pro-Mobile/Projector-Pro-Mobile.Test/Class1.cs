using System;
using Xunit;
using Songs;

namespace Projector_Pro_Mobile.Test
{
    public class SongHandler
    {
        [Fact]
        public void Songs_ShouldInitialize()
        {
            int id = 4;
            string title = "test title";
            int number = 54;
            string key = "B#";
            string body = "text text text";
            Song songItem;

            //songItem = new Song(id, title, number, key, body);

            //Assert.Equal(id, songItem.ID);
            //Assert.Equal(title, songItem.Title);
            //Assert.Equal(number, songItem.Number);
            //Assert.Equal(key, songItem.Key);
            //Assert.Equal(body, songItem.Body);
        }

        [Fact]
        public void SongCollection_ShouldInitialize()
        {
            SongCollection sut;
            string json = @"[{'ID':4,'Title':'He's All I Need','Number':4,'Key':'C','Body':'He's all I need,\r\nHe's all I need,\r\nJesus is all I need;\r\nHe's all I need,\r\nHe's all I need,\r\nJesus is all I need.\r\n\r\nI take Him now,\r\nI take Him now,\r\nFor all that I need;\r\nI take Him now,\r\nI take Him now,\r\nFor all that I need.\r\n\r\nWe worship Thee,\r\nWe worship Thee,\r\nIn spirit and in truth;\r\nWe worship Thee,\r\nWe worship Thee,\r\nIn spirit and in truth.'},{'ID':382,'Title':'He's Everything To Me','Number':4,'Key':'Eb','Body':'He's everything,\r\nHe's everything to me;\r\nHe's everything, \r\nHe's everything to me;\r\nHe's my father, my mother, \r\nMy sister and my brother,\r\nHe's everything to me.'},{'ID':399,'Title':'God Is Moving One More Time','Number':4,'Key':'C','Body':'God is moving one more time,\r\nGod is moving one more time,\r\nGod is moving one more time in the Earth.\r\nTo fullfill His precious Word,\r\nEvery promise we have heard,\r\nGod is moving one more time in the Earth.'},{'ID':788,'Title':'????  Break Thou The Bread Of Life','Number':4,'Key':'Eb','Body':'qiu zhu bo sheng ming bi\r\n??????, Break Thou the bread of life\r\ngong wo xu yao\r\n????, Dear Lord to me\r\nzheng ru dang nian\r\n???? As Thou didst break\r\nni zai hai bin suo xing\r\n??????; The loaves beside the sea\r\n\r\ntou guo sheng jing zi ju\r\n??????, Beyond the sacred page\r\nde jian ni mian\r\n????, I seek Thee Lord\r\nwo ling ke mu\r\n???? My spirit pants for Thee\r\nni di sheng ming zhi dao\r\n??????. Oh living Word\r\n\r\nken qiu yong ni zhen li\r\n??????, Bless Thou the truth, dear Lord\r\ngei wo zhu fu\r\n????, To me, To me\r\nzheng ru dang nian\r\n???? As Thou didst bless\r\nzhu bi zai jia li li\r\n??????; The bread by Galilee\r\n\r\nshi wo yi qie kun bang\r\n?????? Then shall all bondage cease\r\nsuo lian tuo luo\r\n????, All fetters fall\r\nzai ni li mian\r\n???? And I shall find my peace\r\nzhao dao ping an zhi yuan\r\n??????. My All in All\r\n\r\no zhu ni shi wo de\r\n? ????? Thou art the bread of life\r\nsheng ming zhi liang\r\n????, O Lord, to me\r\no zhu ni di hua yu\r\n? ????? Thy holy Word the truth\r\nba wo zheng jiu\r\n????, that saveth me\r\n\r\nwo yuan yu zhu tong huo\r\n??????, Give me to eat and live\r\nzai di ruo tian\r\n????, with Thee above\r\njiao wo ai mu zhen li\r\n?????? Teach me to love Thy truth\r\nyin ni shi ai\r\n????. for Thou art love\r\n\r\nqiu zhu chai qian sheng ling\r\n??????, Oh, send Thy Spirit, Lord\r\nlin dao wo xin\r\n????, now unto me\r\nchu mo wo de ling yan\r\n??????, That He may touch my eyes\r\nshi wo de jian\r\n????; and make me see\r\n\r\nqi shi ni hua yu li\r\n?????? Show me the truth concealed\r\nyong heng zhen li\r\n????, within Thy Word\r\nzai ni de sheng shu zhong\r\n?????? And in Thy Book revealed\r\nneng jian ni mian\r\n????. I see the Lord'},{'ID':804,'Title':'I Love Him','Number':4,'Key':'C','Body':'Gone from my heart\r\nThe world and all its charms,\r\nNow, through the blood, \r\nI'm saved from all alarms,\r\nDown at the cross\r\nMy heart is bending low,\r\nThe precious blood of Jesus\r\nCleanses white as snow. \r\n\r\nCHORUS\r\nI love Him, I love Him\r\nBecause He first loved me\r\nAnd purchased my salvation\r\nOn Calvary's tree.\r\n\r\nOnce I was lost, and way down deep in sin,\r\nOnce was a slave to passions fierce within,\r\nOnce was afraid to trust a loving God,\r\nBut now I'm cleansed from every stain\r\nThrough Jesus' blood. \r\n\r\nOnce I was bound, but now I am set free,\r\nOnce I was blind, but now the light I see,\r\nOnce I was dead, but now in Christ I live,\r\nTo tell the world around\r\nThe peace that He doth give.'},{'ID':1032,'Title':'Saviour, We Remember Thee','Number':4,'Key':'G','Body':'Saviour, we remember Thee!\r\nThy deep woe and agony,\r\nAll Thy suff'ring on the tree;\r\nSaviour, we adore Thee!\r\n\r\nCalvary! O Calvary!\r\nMercy's vast unfathomed sea,\r\nLove, eternal love to me;\r\nSaviour, we adore Thee!\r\n\r\nDarkness hung around Thy head,\r\nWhen for sin Thy blood was shed,\r\nVictim in the sinner's stead;\r\nSaviour, we adore Thee!\r\n\r\nJesus, Lord, Thou now art risen!\r\nThou hast all our sings forgiven;\r\nHaste we to our home in heaven;\r\nSaviour, we adore Thee!\r\n\r\nSoon, with joyful, glad surprise,\r\nWe shall hear Thy Word - \'Arise!\'\r\nMounting upward to the skies'\r\nGlory, glory, glory!\r\n\r\nSaviour, we Thy love adore;\r\nWe will praise Thee more and morel\r\nSpread Thy Name from shore to shore;\r\nSaviour, we adore Thee!'}]";

          //  sut = new SongCollection(json);

            //Assert.Equal(sut.songList[0], new Song());
}
    }
}
