"use client";

import { useEffect, useState } from "react";
import { fetchListings } from "@/lib/api";

interface Listing {
  id: number;
  title: string;
  description: string;
  price: number;
  unitName: string;
}

export default function Home() {
  const [listings, setListings] = useState<Listing[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function getListings() {
      try {
        const data = await fetchListings();
        setListings(data);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    }
    getListings();
  }, []);

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-4xl font-bold mb-8">Cadesto Property Listings</h1>
      {loading && <p>Loading listings...</p>}
      {error && <p className="text-red-500">Error: {error}</p>}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {listings.map((listing) => (
          <div key={listing.id} className="border p-4 rounded shadow">
            <h2 className="text-xl font-semibold">{listing.title}</h2>
            <p className="text-gray-600 mb-2">{listing.description}</p>
            <p className="text-lg font-bold">${listing.price.toLocaleString()} / month</p>
            <p className="text-sm text-gray-500">Unit: {listing.unitName}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
