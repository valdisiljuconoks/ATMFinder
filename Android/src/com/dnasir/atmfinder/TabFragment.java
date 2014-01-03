package com.dnasir.atmfinder;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TabHost;
import android.widget.TabHost.OnTabChangeListener;
import android.widget.TabHost.TabSpec;

public class TabFragment extends Fragment implements OnTabChangeListener {
	
	public static final String TAB_ATMLIST = "AtmList";
	public static final String TAB_ATMMAP = "AtmMap";
	
	private View mRoot;
	private TabHost mTabHost;
	private int mCurrentTab;
	
	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		setRetainInstance(true);
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		mRoot = inflater.inflate(R.layout.tabs_fragment, container);
		mTabHost = (TabHost) mRoot.findViewById(android.R.id.tabhost);
		setupTabs();
		return mRoot;
	}
	
	@Override
	public void onTabChanged(String tabId) {
		if (TAB_ATMLIST.equals(tabId)) {
			mCurrentTab = 0;
			return;
		}
		if (TAB_ATMMAP.equals(tabId)) {
			mCurrentTab = 1;
			return;
		}
	}
	
	private void setupTabs() {
		mTabHost.setup();
		mTabHost.addTab(newTab(TAB_ATMLIST, R.string.tab_list, R.id.tab_1));
		mTabHost.addTab(newTab(TAB_ATMMAP, R.string.tab_map, R.id.tab_2));
	}
	
	private TabSpec newTab(String tag, int labelId, int tabContentId) {
		TabSpec tabSpec = mTabHost.newTabSpec(tag);
		tabSpec.setIndicator(getString(labelId));
		tabSpec.setContent(tabContentId);
		return tabSpec;
	}
}
