package com.dnasir.atmfinder;

import java.util.List;

import android.content.Context;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.android.maps.GeoPoint;
import com.google.android.maps.MapController;
import com.google.android.maps.MapView;
import com.google.android.maps.MyLocationOverlay;
import com.google.android.maps.Overlay;

public class AtmMapFragment extends Fragment
{
	protected ViewGroup mapContainer;
	protected MyLocationOverlay myLocationOverlay;
	//protected Context mContext;
	
	private MapView mapView;
	private MapController mapController;
	private List<Overlay> mapOverlays;
	private LocationManager myLocationManager;
	private LocationListener myLocationListener;
//	private Drawable drawable;
//	private AtmMapItemizedOverlay atmMapItemizedOverlay;
	
	public static AtmMapFragment newInstance()
	{
		AtmMapFragment fragment = new AtmMapFragment();

		Bundle args = new Bundle();
		// add any necessary args here
		fragment.setArguments(args);

		return fragment;
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
	{
		View mRoot = inflater.inflate(R.layout.map_fragment, container, false);
		mapContainer = (ViewGroup) mRoot.findViewById(R.id.map_container);

		mapView = (MapView) mapContainer.findViewById(R.id.mapview);
		mapView.setClickable(true);
		mapView.setBuiltInZoomControls(true);
		
		mapOverlays = mapView.getOverlays();
		myLocationOverlay = new MyLocationOverlay(getActivity(), mapView);
		
		mapController = mapView.getController();
		mapController.setZoom(16);
		mapController.setCenter(new GeoPoint((int) (56.9496 * 1E6), (int) (24.1040 * 1E6)));
		
		myLocationOverlay.enableMyLocation();
		mapOverlays.add(myLocationOverlay);
		
		myLocationManager = (LocationManager)getActivity().getSystemService(Context.LOCATION_SERVICE);
		myLocationListener = new MyLocationListener();
		
		myLocationManager.requestLocationUpdates(
			    LocationManager.GPS_PROVIDER,
			    0,
			    0,
			    myLocationListener);

		return mRoot;
	}
	
//	@Override
//	public void onResume()
//	{
//		super.onCreate(savedInstanceState);
//		myLocationOverlay.enableMyLocation();
//	}
	
	public void showOnMap(GeoPoint location)
	{
		
	}
	
	private void CenterLocation(GeoPoint centerGeoPoint)
	{
		mapController.animateTo(centerGeoPoint);
	}
	
	private class MyLocationListener implements LocationListener
	{

		@Override
		public void onLocationChanged(Location location)
		{
			GeoPoint myGeoPoint = new GeoPoint((int)(location.getLatitude()*1E6), (int)(location.getLongitude()*1E6));
			
			CenterLocation(myGeoPoint);
		}

		@Override
		public void onProviderDisabled(String provider)
		{
			// TODO Auto-generated method stub
			
		}

		@Override
		public void onProviderEnabled(String provider)
		{
			// TODO Auto-generated method stub
			
		}

		@Override
		public void onStatusChanged(String provider, int status, Bundle extras)
		{
			// TODO Auto-generated method stub
			
		}
		
	}
}
