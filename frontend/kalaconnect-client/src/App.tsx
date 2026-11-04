import { useEffect, useState } from "react";
import api from "./services/api";
import "./App.css";

interface HealthResponse {
    application: string;
    status: string;
    version: string;
}

interface DatabaseResponse {
    database: string;
    connected: boolean;
}

function App() {

    const [health, setHealth] = useState<HealthResponse | null>(null);
    const [database, setDatabase] =
        useState<DatabaseResponse | null>(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        const checkConnection = async () => {
            try {
                const healthResponse =
                    await api.get<HealthResponse>("/health");

                const databaseResponse =
                    await api.get<DatabaseResponse>("/db-check");

                setHealth(healthResponse.data);
                setDatabase(databaseResponse.data);

            } catch (error) {
                console.error("API connection failed:", error);
            } finally {
                setLoading(false);
            }
        };

        checkConnection();

    }, []);

    return (
        <div className="app">

            <header className="header">
                <h1>🎨 KalaConnect</h1>
                <p>Discover. Create. Connect. Sell.</p>
            </header>

            <main className="dashboard">

                <h2>Development Dashboard</h2>

                {loading ? (
                    <p>Checking connections...</p>
                ) : (
                    <>
                        <div className="status-card">
                            <h3>Backend API</h3>

                            <p>
                                {health?.application ?? "Unavailable"}
                            </p>

                            <span>
                                {health?.status ?? "Disconnected"}
                            </span>
                        </div>

                        <div className="status-card">
                            <h3>SQL Server</h3>

                            <p>
                                {database?.database ?? "Unavailable"}
                            </p>

                            <span>
                                {database?.connected
                                    ? "Connected"
                                    : "Disconnected"}
                            </span>
                        </div>
                    </>
                )}

            </main>

        </div>
    );
}

export default App;