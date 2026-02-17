import React, { useState } from "react"
import type {LoginRequest}  from '../components/type_files'; // interface dosyanın yolu
import {loginService} from '../services/api';
import { Link } from 'react-router-dom';

const Login = () => {  
  const [formdata ,setFormdata] =useState<LoginRequest>({
        email : '',
        password :''
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormdata(prev => ({ ...prev, [name]: value }));
    
  };


   const handleSubmit = async (e: React.FormEvent) =>{
      e.preventDefault();
      console.log("giriş yapılıyor:" , formdata);
      await loginService(formdata);
     window.location.reload(); 
        
    }

   

    return (
    <div className="min-h-screen bg-zinc-950 flex items-center justify-center p-4">
      <div className="bg-zinc-900 p-8 rounded-xl border-l-4 border-red-600 shadow-2xl w-full max-w-md">
        <h2 className="text-3xl font-black text-white uppercase mb-6">Giriş Yap</h2>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="text-zinc-400 text-sm uppercase">Email</label>
            <input 
              name="email"
              type="email" 
              onChange={handleChange}
              className="w-full bg-zinc-800 border border-zinc-700 p-3 rounded text-white focus:border-red-600 outline-none transition-all"
              placeholder="f1@pilot.com"
            />
          </div>
          <div>
            <label className="text-zinc-400 text-sm uppercase">Şifre</label>
            <input 
              name="password"
              type="password" 
              onChange={handleChange}
              className="w-full bg-zinc-800 border border-zinc-700 p-3 rounded text-white focus:border-red-600 outline-none transition-all"
              placeholder="••••••••"
            />
          </div>
          <div className="mt-6 text-center">
            <p className="text-zinc-400 text-sm">
              Henüz hesabın yok mu?{' '}
            <Link 
              to="/register" 
              className="text-red-500 font-bold hover:text-red-400 hover:underline transition-all"
            >
            Pite Gir (Kayıt Ol)
          </Link>
  </p>
</div>
          <button className="w-full bg-white hover:bg-blue-400 text-black font-bold py-3 px-4 rounded transition-all mt-4 flex items-center justify-center">
            <img src={`/src/assets/f1.png`} alt="f1" className="h-10 w-auto object-contain"/>
            
            <span className="uppercase tracking-widest">  GİRİŞ YAP  </span>
            
            <img src={`/src/assets/f1.png`} alt="f1" className="h-10 w-auto object-contain"/>
          </button>
        </form>
      </div>
    </div>
  );
}
export default Login;