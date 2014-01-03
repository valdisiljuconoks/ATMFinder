package com.dnasir.atmfinder;

import com.google.android.maps.GeoPoint;

public class Atm
{
	public String ChainName;
	public String ItemName;
	public String ItemAddress;
	public GeoPoint Coordinates;
	public int ItemFeatures; // 1 - Cash out, 2 - Cash in, 4 - Office
}
