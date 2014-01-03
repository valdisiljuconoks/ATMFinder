package com.dnasir.atmfinder;

import java.util.ArrayList;

public class DataSource
{
	static final int item_count = 100;
	ArrayList<Atm> items;
	
	// TODO: Actually fetch data from a web service
	public DataSource() {
        items = new ArrayList<Atm>(item_count);
        for( int i = 0 ; i < item_count ; ++i ){
        	Atm newItem = new Atm();
        	newItem.ChainName = "Chain No." + i;
        	newItem.ItemName = "Bank No." + i;
        	newItem.ItemAddress = i + " Lorem St.";
            items.add(newItem);
        }
    }

    public int getItemCount() {
    	try {
            Thread.sleep( 500L );
        } catch( InterruptedException ex ) {}
        return items.size();
    }

    public Atm getItem( int itemIndex ) {
    	// It's a slow data source
        try {
            Thread.sleep( 500L );
        } catch( InterruptedException ex ) {}
        return items.get( itemIndex );
    }
    
    public ArrayList<Atm> getItems(int page, int itemsPerPage)
    {
    	ArrayList<Atm> atms = new ArrayList<Atm>();
    	try {
            Thread.sleep( 500L );
        } catch( InterruptedException ex ) {}
    	
    	int first = (page - 1) > -1 ? (page - 1) : 0;
    	int last = ((first*itemsPerPage) + itemsPerPage) > items.size() ? items.size() : ((first*itemsPerPage) + itemsPerPage);
    	
    	for(int i = (first*itemsPerPage); i < last; i++)
    	{
    		atms.add(items.get(i));
    	}
    	
    	return atms;
    }
}
