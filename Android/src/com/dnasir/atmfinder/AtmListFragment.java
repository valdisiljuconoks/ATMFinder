package com.dnasir.atmfinder;

import java.util.ArrayList;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AbsListView.OnScrollListener;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class AtmListFragment extends Fragment implements OnScrollListener
{
	private Context context;
	private View mRoot;
	private ListView mListView;
	private ArrayList<Atm> atmListItems;
	private AtmListAdapter adapter;
	private int itemsPerPage = 15;
	private int currentPage = 1;
	private boolean asyncTaskRunning;
	private DataSource dataSource;
	private boolean loadMore;
	
	@Override
	public void onAttach(Activity activity)
	{
		super.onAttach(activity);
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
	{
		this.context = getActivity().getApplicationContext();
		this.mRoot = inflater.inflate(R.layout.list_fragment, container, false);
		this.mListView = (ListView) mRoot.findViewById(R.id.atmListView);
		this.atmListItems = new ArrayList<Atm>();
		this.adapter = new AtmListAdapter(context, R.layout.atm_list_item, atmListItems);
		this.mListView.setAdapter(adapter);
		this.mListView.setOnScrollListener(this);
		this.asyncTaskRunning = false;
		this.dataSource = new DataSource();
		this.loadMore = false;
		
		// Initial fetch
		new AtmListLoader(this.adapter).execute();
		
		return mRoot;
	}

	@Override
	public void onScroll(AbsListView view, int firstVisible, int visibleCount, int totalCount)
	{
		this.loadMore = firstVisible + visibleCount >= totalCount;
	}

	@Override
	public void onScrollStateChanged(AbsListView view, int scrollState)
	{
		if(!this.asyncTaskRunning && this.loadMore)
		{
			new AtmListLoader(this.adapter).execute();
		}
	}
	
	private class AtmListLoader extends AsyncTask<Void, Void, Void>
	{
		private ArrayList<Atm> items;
		private AtmListAdapter adapter;
		
		public AtmListLoader(AtmListAdapter adapter)
		{
			this.adapter = adapter;
			this.items = new ArrayList<Atm>();
		}

		@Override
        protected void onPreExecute() 
        {
			if(asyncTaskRunning)
			{
				this.cancel(true);
				return;
			}
			getActivity().setProgressBarIndeterminateVisibility(true);
			asyncTaskRunning = true;
        }
		
		@Override
		protected Void doInBackground(Void... arg0)
		{
			if(this.isCancelled())
			{
				return null;
			}
			
			this.items = dataSource.getItems(currentPage, itemsPerPage);
			if(this.items != null && this.items.size() > 0)
			{
				atmListItems.addAll(this.items);
				currentPage++;
			}
			
			return null;
		}
		
		@Override
        protected void onPostExecute(Void result) 
        {
			if(this.isCancelled())
			{
				return;
			}
			adapter.notifyDataSetChanged();
			asyncTaskRunning = false;
			getActivity().setProgressBarIndeterminateVisibility(false);
        }
	}
	
	private class AtmListAdapter extends ArrayAdapter<Atm>
	{
		private Context context;
		private int layoutResourceId;
		private ArrayList<Atm> atms;
		
		public AtmListAdapter(Context context, int layoutResourceId, ArrayList<Atm> atms)
		{
			super(context, layoutResourceId, atms);
			this.context = context;
			this.layoutResourceId = layoutResourceId;
			this.atms = atms;
		}
		
		@Override
		public View getView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			AtmListItemView atmListItemView = null;
			
			if(row == null)
			{
				LayoutInflater inflater = getActivity().getLayoutInflater();
				row = inflater.inflate(layoutResourceId, parent, false);
				atmListItemView = new AtmListItemView();
				atmListItemView.ChainLogo = (ImageView)row.findViewById(R.id.chainLogo);
				atmListItemView.ChainName = (TextView)row.findViewById(R.id.chainName);
				atmListItemView.ItemDetails = (TextView)row.findViewById(R.id.itemDetails);
				
				row.setTag(atmListItemView);
			}
			else
			{
				atmListItemView = (AtmListItemView)row.getTag();
			}
			
			Atm atm = atms.get(position);
			atmListItemView.ChainName.setText(atm.ChainName);
			atmListItemView.ItemDetails.setText(atm.ItemAddress);
			
			return row;
		}
	}
}
