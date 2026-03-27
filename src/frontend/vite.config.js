import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// Dev local: backend roda na porta 5253 (launchSettings.json)
// Container : VITE_BACKEND_URL não é usado pelo nginx (proxy interno)
const backendUrl = process.env.VITE_BACKEND_URL || "http://localhost:5253";

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      "/api": {
        target: backendUrl,
        changeOrigin: true,
        secure: false
      }
    }
  }
});

