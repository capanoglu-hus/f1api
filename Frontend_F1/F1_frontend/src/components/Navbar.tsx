import React from "react";
import { NavLink } from "react-router";

const Navbar: React.FC = () => {
  return (
    <nav className="bg-zinc-900 text-white shadow-lg">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo Kısmı */}
          <div className="shrink-0 flex items-center">
            <span className="text-2xl font-bold tracking-tighter text-red-600">
              F1<span className="text-white underline decoration-red-600">PRO</span>
            </span>
          </div>

          {/* Menü Linkleri */}
          <div className="hidden md:block">
            <div className="ml-10 flex items-baseline space-x-4">
                  <NavLink to="/" className="px-3 py-2 rounded-md text-sm font-medium hover:bg-zinc-800 hover:text-red-500 transition-colors duration-200" >
                 Home
                </NavLink>
                <NavLink to="/Drivers" className="px-3 py-2 rounded-md text-sm font-medium hover:bg-zinc-800 hover:text-red-500 transition-colors duration-200" >
                    Drivers
                </NavLink>
                <NavLink to="/Teams" className="px-3 py-2 rounded-md text-sm font-medium hover:bg-zinc-800 hover:text-red-500 transition-colors duration-200" >
                    Teams
                </NavLink>
                <NavLink to="/Races" className="px-3 py-2 rounded-md text-sm font-medium hover:bg-zinc-800 hover:text-red-500 transition-colors duration-200" >
                    Races
                </NavLink>
                <NavLink to="/User" className="px-3 py-2 rounded-md text-sm font-medium hover:bg-zinc-800 hover:text-red-500 transition-colors duration-200" >
                    User
                </NavLink>
                <NavLink to="/Prediction" className="px-3 py-2 rounded-md text-sm font-medium hover:bg-zinc-800 hover:text-red-500 transition-colors duration-200" >
                    Prediction
                </NavLink>
            </div>
          </div>

         
        </div>
      </div>
    </nav>
  );
};

export default Navbar;