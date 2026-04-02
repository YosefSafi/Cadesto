const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7198/api";

export async function fetchListings() {
  const response = await fetch(`${API_URL}/public/listings`);
  if (!response.ok) throw new Error("Failed to fetch listings");
  return response.json();
}

export async function login(email: string, password: string) {
  const response = await fetch(`${API_URL}/public/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  });
  if (!response.ok) throw new Error("Login failed");
  return response.json();
}
