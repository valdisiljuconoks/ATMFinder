package com.dnasir.atmfinder;

import android.app.Dialog;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.Window;

public class MainActivity extends FragmentActivity
{
	@Override
	public void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_INDETERMINATE_PROGRESS);
		setContentView(R.layout.activity_main);
		
		setProgressBarIndeterminateVisibility(false);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		getMenuInflater().inflate(R.menu.activity_main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		switch (item.getItemId())
		{
		case R.id.menu_about:
			openAboutDialog();
			return true;
		default:
			return super.onOptionsItemSelected(item);
		}
	}

	private void openAboutDialog()
	{
		final Dialog dialog = new Dialog(MainActivity.this);
		dialog.setContentView(R.layout.menu_about);
		dialog.setTitle(R.string.dialog_about);
		dialog.show();
	}
}
