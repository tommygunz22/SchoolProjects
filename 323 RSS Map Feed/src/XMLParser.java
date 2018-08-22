import java.io.*;
import java.net.*;
import java.util.*;
import de.nava.informa.core.ChannelIF;
import de.nava.informa.core.ItemIF;
import de.nava.informa.impl.basic.ChannelBuilder;
import de.nava.informa.parsers.FeedParser;




public class XMLParser extends RSSManagerFrame {


	  public static void parser( String link )
	  {
		  
		 
	    try
	    {
	      URL url = new URL(link ); 
	      ChannelIF channel = FeedParser.parse( new ChannelBuilder(), url );
	      System.out.println( "Channel: " + channel.getTitle() );
	      System.out.println( "Description: " + channel.getDescription() );
	      System.out.println( "PubDate: " + channel.getPubDate() );
	      Collection items = channel.getItems();
	      for( Iterator i=items.iterator(); i.hasNext(); )
	      {
	        ItemIF item = ( ItemIF )i.next();
	        System.out.println( item.getTitle() );
	        System.out.println( "\t" + item.getDescription() );
	        System.out.println( "Categories: " + item.getCategories() );
	        System.out.println( "Date: " + item.getDate() );
	        System.out.println( "Link: " + item.getLink() + "\n" );
	      }
	    }
	    catch( Exception e )
	    {
	      e.printStackTrace();
	    }
	  }
	
}
