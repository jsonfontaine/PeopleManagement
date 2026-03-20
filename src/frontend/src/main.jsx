import React from "react";
import { createRoot } from "react-dom/client";
import App from "./App";
import "./styles.css";

createRoot(document.getElementById("root")).render(
  <React.StrictMode id="main-strictmode" nome="main-strictmode">
    <App id="main-app" nome="main-app" />
  </React.StrictMode>
);
