using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yelp2
{
    public partial class Form2 : Form
    {
        MySQL_Connection mydb;
        string bid;
        public Form2(List<String> s, List<String> categories)
        {
            bid = s[0];
            InitializeComponent();
            mydb = new MySQL_Connection();
            List<String> originalbidcategorylist;
            List<String> originalreviewcount;
            List<String> businesslist;
            List<String> originallatlist;
            List<String> originalonglist;
            List<String> finalreviewlist;
            List<String> finalscorelist;
            List<String> finaldistancelist;
            List<String> avgratinglist;
            List<String> avgratinglist2;
            List<String> avgstarlist;
            String avgstarstring = "";
            String originalbidstring = "";
            String getbusinessstring = "";
            String originalareastring = "";
            String maketemptable = "";
            String insertstatement = "";
            String namefinalstring = "";
            String reviewfinalstring = "";
            String originalreviewcountstring = "";
            String scorestring = "";
            String insertdistance = "";
            maketemptable += "Create temporary table businesstemp (bid VARCHAR(26) primary key , name VARCHAR(75), lat float, lon float, reviewcount int(11), score int(11), distance VARCHAR(10) default '', avgrating float);";
            mydb.createsqldatabase(maketemptable);
            originalbidstring += "select category from categories where bid = '" + bid + "';";
            originalreviewcountstring += "select reviewcount from business where bid = '" + bid + "';";
            originalareastring += "select latitude from business where bid = '" + bid + "';";
            originallatlist = mydb.Sqlopenconnection(originalareastring, "latitude");
            originalareastring = "";
            originalareastring += "select longitude from business where bid = '" + bid + "';";
            originalonglist = mydb.Sqlopenconnection(originalareastring, "longitude");


            

            originalbidcategorylist = mydb.Sqlopenconnection(originalbidstring, "category");
            originalreviewcount = mydb.Sqlopenconnection(originalreviewcountstring, "reviewcount");
         
            
           

            insertstatement += "INSERT into businesstemp(bid, name, lat, lon, reviewcount) "+
            " select distinct bid, name, latitude, longitude, reviewcount from business where business.bid in (select bid from categories where category = '" + originalbidcategorylist[0].ToString() + "' ";
            for(int i =1; i < originalbidcategorylist.Count; i++)
            {
                insertstatement += "or category = '" + originalbidcategorylist[i].ToString() + "' ";
                
                
            }
            insertstatement += ") and business.bid <> '" + bid + "';";
            mydb.insertsqldatabase(insertstatement);

            insertstatement = "";


            insertstatement += "update businesstemp set avgrating = (select avg(stars) from review where review.bid = '" + bid + "' )";

            mydb.insertsqldatabase(insertstatement);

            insertstatement = "";



            insertstatement += "update businesstemp set score = 1 where businesstemp.bid in (select bid from categories where category = '" + originalbidcategorylist[0].ToString() + "' ";

            for (int i = 1; i < originalbidcategorylist.Count; i++)
            {
                insertstatement += "or category = '" + originalbidcategorylist[i].ToString() + "' ";
                

            }
            insertstatement += ");";
            mydb.insertsqldatabase(insertstatement);

            insertstatement = "";

            insertstatement += "update businesstemp set score = 5 where businesstemp.bid in (select bid from categories where category = '" + originalbidcategorylist[0].ToString() + "') ";

            for (int i = 1; i < originalbidcategorylist.Count; i++)
            {
                insertstatement += "and businesstemp.bid in (select bid from categories where category = '" + originalbidcategorylist[i].ToString() + "') ";


            }
            insertstatement += ";";
            mydb.insertsqldatabase(insertstatement);



            insertstatement = "";

            insertstatement += "update businesstemp set score = 5 where businesstemp.bid in (select bid from categories where category = '" + categories[0].ToString() + "') ";

            for (int i = 1; i < categories.Count; i++)
            {
                insertstatement += "and businesstemp.bid in (select bid from categories where category = '" + categories[i].ToString() + "') ";


            }
            insertstatement += ";";
            mydb.insertsqldatabase(insertstatement);

            insertstatement = "";

            insertstatement += "update  businesstemp set score = score + 1  where ( 3959 * acos( cos( radians('" + originallatlist[0].ToString() + "') )" +
                "* cos(radians( businesstemp.lat ) ) * cos( radians( businesstemp.lon ) - radians("
                + "'" + originalonglist[0].ToString() + "' ) ) + sin( radians( '" + originallatlist[0].ToString() + "') ) *sin( radians( businesstemp.lat ) ) ) ) < 25; ";

            insertdistance += "update  businesstemp set distance = '<25m' where ( 3959 * acos( cos( radians('" + originallatlist[0].ToString() + "') )" +
                "* cos(radians( businesstemp.lat ) ) * cos( radians( businesstemp.lon ) - radians("
                + "'" + originalonglist[0].ToString() + "' ) ) + sin( radians( '" + originallatlist[0].ToString() + "') ) *sin( radians( businesstemp.lat ) ) ) ) < 25; ";

            mydb.insertsqldatabase(insertstatement);
            mydb.insertsqldatabase(insertdistance);

            insertstatement = "";
            insertdistance = "";

            insertstatement += "update  businesstemp set score = score + 2 where ( 3959 * acos( cos( radians('" + originallatlist[0].ToString() + "') )" +
               "* cos(radians( businesstemp.lat ) ) * cos( radians( businesstemp.lon ) - radians("
               + "'" + originalonglist[0].ToString() + "' ) ) + sin( radians( '" + originallatlist[0].ToString() + "') ) *sin( radians( businesstemp.lat ) ) ) ) < 15; ";

            insertdistance += "update  businesstemp set distance = '<15m' where ( 3959 * acos( cos( radians('" + originallatlist[0].ToString() + "') )" +
               "* cos(radians( businesstemp.lat ) ) * cos( radians( businesstemp.lon ) - radians("
               + "'" + originalonglist[0].ToString() + "' ) ) + sin( radians( '" + originallatlist[0].ToString() + "') ) *sin( radians( businesstemp.lat ) ) ) ) < 15; ";

            mydb.insertsqldatabase(insertstatement);
            mydb.insertsqldatabase(insertdistance);

            insertstatement = "";
            insertdistance = "";

            insertstatement += "update  businesstemp set score = score + 3 where ( 3959 * acos( cos( radians('" + originallatlist[0].ToString() + "') )" +
               "* cos(radians( businesstemp.lat ) ) * cos( radians( businesstemp.lon ) - radians("
               + "'" + originalonglist[0].ToString() + "' ) ) + sin( radians( '" + originallatlist[0].ToString() + "') ) *sin( radians( businesstemp.lat ) ) ) ) < 5; ";

            insertdistance += "update  businesstemp set distance = '<5m' where ( 3959 * acos( cos( radians('" + originallatlist[0].ToString() + "') )" +
               "* cos(radians( businesstemp.lat ) ) * cos( radians( businesstemp.lon ) - radians("
               + "'" + originalonglist[0].ToString() + "' ) ) + sin( radians( '" + originallatlist[0].ToString() + "') ) *sin( radians( businesstemp.lat ) ) ) ) < 5; ";

            mydb.insertsqldatabase(insertstatement);
            mydb.insertsqldatabase(insertdistance);

            insertstatement = "";
            insertdistance = "";
            int number = 0;
            int lower = 0;
            String reviewcounttoint = originalreviewcount[0].ToString();
            number = int.Parse(reviewcounttoint);
            lower = int.Parse(reviewcounttoint);
           number += 50;
            lower -= 50;
            if (lower < 0)
                lower = 0;
            insertstatement += "update businesstemp set score = score + 2 where businesstemp.reviewcount between " + number.ToString() + " and " + lower.ToString() + ";";
            mydb.insertsqldatabase(insertstatement);

            namefinalstring += "select name from businesstemp order by score desc limit 75;";
            reviewfinalstring += "select reviewcount from businesstemp order by score desc limit 75;";
            scorestring += "select score from businesstemp order by score desc limit 75;";
            insertdistance += "select distance from businesstemp order by score desc limit 75;";
            avgstarstring += "select avgstar from businesstemp, business_average_ratings where businesstemp.bid = business_average_reviews.bid;";

            insertstatement = "";

            insertstatement += "select bid from businesstemp order by score desc limit 75;";

          

            avgratinglist = mydb.Sqlopenconnection(insertstatement, "bid");


            insertstatement = "";

            insertstatement += "select avgstar from business_average_reviews where business_average_reviews.bid in (select bid from businesstemp order by score desc ) limit 75;";

            avgratinglist2 = mydb.SQLSELECTExec3(insertstatement, "avgstar"); //final businesses avg stars*/

            businesslist = mydb.Sqlopenconnection(namefinalstring, "name");
            finalreviewlist = mydb.Sqlopenconnection(reviewfinalstring, "reviewcount");
            finalscorelist = mydb.Sqlopenconnection(scorestring, "score");
            finaldistancelist = mydb.Sqlopenconnection(insertdistance, "distance");
            
     
            for (int i = 0; i < businesslist.Count(); i++)
            {

                dataGridView1.Rows.Add(businesslist[i], finaldistancelist[i], finalreviewlist[i], avgratinglist2[i],"", finalscorelist[i]);
               // getbusinessstring += "or category = '" + originalbidcategorylist[i].ToString() + "' ";
            }

            mydb.insertsqldatabase("drop table businesstemp;");
            mydb.CloseConnection();

        }

       
    }
}







/* insertstatement += "select bid from businesstemp group by score desc;";

 //insertstatement = "";

 avgratinglist = mydb.Sqlopenconnection(insertstatement, "bid");

 insertstatement = "";

 insertstatement += " select avg(review.stars) from review where review.bid  ='" + avgratinglist[0].ToString() + "' ";
 for (int i = 1; i < avgratinglist.Count; i++ )
 {
     insertstatement += " or review.bid = '" + avgratinglist[i].ToString() + "' ";
 }
 insertstatement += " group by review.bid;";

 avgratinglist2 = mydb.SQLSELECTExec3(insertstatement, "stars"); //final businesses avg stars*/



/* insertstatement = "";

 for (int i = 0; i < avgratinglist.Count; i++)
 {
     insertstatement = "update businesstemp set avgrating = (select avg(review.stars) from review where review.bid ='" + avgratinglist[i].ToString() + "' ) where businesstemp.bid = '" + avgratinglist[i].ToString() + "';";
     mydb.insertsqldatabase(insertstatement);
 }
 insertstatement = "";
 insertstatement += "update businesstemp set score = score + 2 where businesstemp.avgrating between (select avg(review.stars) + 0.5 from review where review.bid = '" + bid + "') and (select avg(review.stars) - 0.5 from review where review.bid = '" + bid + "');";

 mydb.insertsqldatabase(insertstatement);*/