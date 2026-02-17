
import React, { useEffect, useState } from 'react';
import type {DriverTypes,DriverForVote}  from '../components/type_files'; // interface dosyanın yolu
import {getDrivers,driverVote,driverVoteBool} from '../services/api';

const Drivers: React.FC = () => {
  const [drivers, setDrivers] = useState<DriverTypes[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [bool ,setBool] = useState<boolean>(false);

  useEffect(() => {
    const fetchDrivers = async () => {
      try {
        const data = await getDrivers();
        setDrivers(data);
      } catch (error) {
        console.error("Sürücüler yüklenirken hata oluştu:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchDrivers();
  }, []);

  useEffect(() => {
    const driverVotebool = async () => {
      try {
        const data = await driverVoteBool();
        setBool(data);
      } catch (error) {
        console.error("Sürücüler yüklenirken hata oluştu:", error);
      } finally {
        setLoading(false);
      }
    };

    driverVotebool();
  }, []);
  

  const [formdata, setFormdata] = useState<DriverForVote>({
    firstDriverId: 0,
    secondDriverId: 0,
    thirdDriverId: 0,
  });
  const handleSubmit = async (e: React.FormEvent) =>{
    e.preventDefault();
    console.log("OY VERİLDİ:" , formdata);
    await driverVote(formdata);
    window.location.reload(); 
  }
  
  const handleChange = (e: React.ChangeEvent<HTMLInputElement| HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormdata(prev => ({ ...prev, [name]: Number(value) }));
      
  };
  
  
  
  
if (loading) return <div className="p-8 text-white">Yükleniyor...</div>;
  return (
  <div className="min-h-screen bg-zinc-950 p-6">
  {/* 1. OYLAMA BÖLÜMÜ */}
{bool && (
  <div className="mb-12 animate-in fade-in slide-in-from-top duration-700">
    <div className="max-w-4xl mx-auto bg-zinc-900 border border-zinc-800 rounded-3xl p-8 shadow-2xl">
      <h2 className="text-2xl font-black text-white uppercase tracking-tighter mb-6 flex items-center gap-2">
        <span className="w-2 h-8 bg-red-600 rounded-full"></span>
        Günün Sürücülerini Seç
      </h2>
      
      <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-3 gap-6">
        
        {/* Seçim Kutuları */}
        {[
          { id: "firstDriverId", label: "1. Sürücü Seçimi" },
          { id: "secondDriverId", label: "2. Sürücü Seçimi" },
          { id: "thirdDriverId", label: "3. Sürücü Seçimi" }
        ].map((item) => (
          <div key={item.id} className="space-y-2">
            <label className="text-zinc-500 text-xs font-bold uppercase ml-1">{item.label}</label>
            <div className="relative">
              <select 
                name={item.id}
                onChange={handleChange}
                // value={formdata[item.id]} // State ile tam senkronize olması için opsiyonel
                className="w-full bg-zinc-800 border border-zinc-700 p-4 rounded-xl text-white focus:border-red-600 focus:ring-1 focus:ring-red-600 outline-none transition-all appearance-none cursor-pointer"
              >
                <option value="0">Sürücü Seçiniz...</option>
                {drivers?.map((driver) => (
                  <option key={driver.id} value={driver.id} className="bg-zinc-900">
                    #{driver.racingNumber} - {driver.name}
                  </option>
                ))}
              </select>
              {/* Özel Ok İkonu (Select'in varsayılan okunu 'appearance-none' ile sildiğimiz için) */}
              <div className="absolute inset-y-0 right-4 flex items-center pointer-events-none">
                <svg className="w-4 h-4 text-zinc-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" />
                </svg>
              </div>
            </div>
          </div>
        ))}

        <div className="md:col-span-3">
          <button className="w-full bg-red-600 hover:bg-red-700 text-white font-black py-4 rounded-xl transition-all uppercase tracking-[0.2em] shadow-lg shadow-red-900/20 active:scale-[0.98]">
            Oylamayı Tamamla
          </button>
        </div>
      </form>
    </div>
  </div>
)}

  {/* 2. SÜRÜCÜ LİSTESİ (Stabil Grid) */}
  <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-2 gap-8">
    {drivers?.map((driver) => (
    <div 
      key={driver.id} 
      className="group relative bg-zinc-900 border border-zinc-800 rounded-3xl p-6 flex items-center justify-between overflow-hidden hover:border-red-600 transition-all duration-500 shadow-2xl"
    >
     
      <div className="absolute top-2 right-6 select-none">
        <span className="text-8xl font-black italic text-zinc-800/50 group-hover:text-red-600 transition-colors duration-500">
          #{driver.racingNumber}
        </span>
      </div>

    
      <div className="flex-1 z-10 pr-4">
        
        <h2 className="text-4xl font-black text-white uppercase tracking-tighter leading-none mb-2 group-hover:text-red-500 transition-colors">
          {driver.name}
        </h2>
        
        
        <p className="text-xl font-bold text-zinc-400 uppercase tracking-widest mb-4">
          {driver.team}
        </p>

        
        <p className="text-sm text-zinc-500 font-medium leading-relaxed max-w-62.5 line-clamp-2">
          {driver.description}
        </p>
      </div>

      
      <div className="flex-1 z-8 pr-10 ">
        
        <div className="absolute inset-2 bg-red-600/10 blur-3xl rounded-full group-hover:bg-red-600/20 transition-all"></div>
        
        <img 
          // Assets klasöründeki dosya adını API'den gelen imgUrl ile eşleştiriyoruz
          src={`/src/assets/${driver.imgUrl}`} 
          alt={driver.name}
          className="relative z-10 w-full h-full transform group-hover:scale-110 group-hover:-translate-y-2 transition-all duration-450"
          onError={(e) => { (e.target as HTMLImageElement).src = '/src/assets/default-driver.png' }}
        />
      </div>
    </div>
  ))}
  </div>
</div>
  );
};

export default Drivers;


/*
 }
  {bool && (
    <div className="mb-12 animate-in fade-in slide-in-from-top duration-700">
      <div className="max-w-4xl mx-auto bg-zinc-900 border border-zinc-800 rounded-3xl p-8 shadow-2xl">
        <h2 className="text-2xl font-black text-white uppercase tracking-tighter mb-6 flex items-center gap-2">
          <span className="w-2 h-8 bg-red-600 rounded-full"></span>
          Günün Sürücülerini Seç
        </h2>
        
        <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-3 gap-6">
          <div className="space-y-2">
            <label className="text-zinc-500 text-xs font-bold uppercase ml-1">1. Sürücü ID</label>
            <input 
              name="firstDriverId"
              type="number" 
              onChange={handleChange}
              className="w-full bg-zinc-800 border border-zinc-700 p-4 rounded-xl text-white focus:border-red-600 focus:ring-1 focus:ring-red-600 outline-none transition-all"
              placeholder="Örn: 1"
            />
          </div>
          <div className="space-y-2">
            <label className="text-zinc-500 text-xs font-bold uppercase ml-1">2. Sürücü ID</label>
            <input 
              name="secondDriverId"
              type="number" 
              onChange={handleChange}
              className="w-full bg-zinc-800 border border-zinc-700 p-4 rounded-xl text-white focus:border-red-600 focus:ring-1 focus:ring-red-600 outline-none transition-all"
              placeholder="Örn: 44"
            />
          </div>
          <div className="space-y-2">
            <label className="text-zinc-500 text-xs font-bold uppercase ml-1">3. Sürücü ID</label>
            <input 
              name="thirdDriverId"
              type="number" 
              onChange={handleChange}
              className="w-full bg-zinc-800 border border-zinc-700 p-4 rounded-xl text-white focus:border-red-600 focus:ring-1 focus:ring-red-600 outline-none transition-all"
              placeholder="Örn: 14"
            />
          </div>
          <div className="md:col-span-3">
            <button className="w-full bg-red-600 hover:bg-red-700 text-white font-black py-4 rounded-xl transition-all uppercase tracking-[0.2em] shadow-lg shadow-red-900/20 active:scale-[0.98]">
              Oylamayı Tamamla
            </button>
          </div>
        </form>
      </div>
    </div>
  )}

*/



/*<div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-2 gap-8">
    <div>
      {bool ? (
        <form onSubmit={handleSubmit} >
                <div>
                  <label className="text-zinc-400 text-sm uppercase">firstDriverId</label>
                  <input 
                    name="firstDriverId"
                    type="number" 
                    onChange={handleChange}
                    className="w-full bg-zinc-800 border border-zinc-700 p-3 rounded text-white focus:border-red-600 outline-none transition-all"
                    placeholder="f1@firstDriverId"
                  />
                </div>
                <div>
                  <label className="text-zinc-400 text-sm uppercase">secondDriverId</label>
                  <input 
                    name="secondDriverId"
                    type="number" 
                    onChange={handleChange}
                    className="w-full bg-zinc-800 border border-zinc-700 p-3 rounded text-white focus:border-red-600 outline-none transition-all"
                    placeholder="••••••••"
                  />
                </div>
                <div>
                  <label className="text-zinc-400 text-sm uppercase">thirdDriverId</label>
                  <input 
                    name="thirdDriverId"
                    type="number" 
                    onChange={handleChange}
                    className="w-full bg-zinc-800 border border-zinc-700 p-3 rounded text-white focus:border-red-600 outline-none transition-all"
                    placeholder="••••••••"
                  />
                </div>
              <button className="w-full bg-white hover:bg-blue-400 text-black font-bold py-3 px-4 rounded transition-all mt-4 flex items-center justify-center">
                  -
                  
                  <span className="uppercase tracking-widest">  OY VER  </span>
                  
                  -
                </button>
      </form>
      ) : (
          <div></div>
      )
      }
      
    </div>
    
  
  {drivers?.map((driver) => (
    <div 
      key={driver.id} 
      className="group relative bg-zinc-900 border border-zinc-800 rounded-3xl p-6 flex items-center justify-between overflow-hidden hover:border-red-600 transition-all duration-500 shadow-2xl"
    >
     
      <div className="absolute top-2 right-6 select-none">
        <span className="text-8xl font-black italic text-zinc-800/50 group-hover:text-red-600 transition-colors duration-500">
          #{driver.racingNumber}
        </span>
      </div>

    
      <div className="flex-1 z-10 pr-4">
        
        <h2 className="text-4xl font-black text-white uppercase tracking-tighter leading-none mb-2 group-hover:text-red-500 transition-colors">
          {driver.name}
        </h2>
        
        
        <p className="text-xl font-bold text-zinc-400 uppercase tracking-widest mb-4">
          {driver.team}
        </p>

        
        <p className="text-sm text-zinc-500 font-medium leading-relaxed max-w-62.5 line-clamp-2">
          {driver.description}
        </p>
      </div>

      
      <div className="flex-1 z-8 pr-10 ">
        
        <div className="absolute inset-2 bg-red-600/10 blur-3xl rounded-full group-hover:bg-red-600/20 transition-all"></div>
        
        <img 
          // Assets klasöründeki dosya adını API'den gelen imgUrl ile eşleştiriyoruz
          src={`/src/assets/${driver.imgUrl}`} 
          alt={driver.name}
          className="relative z-10 w-full h-full transform group-hover:scale-110 group-hover:-translate-y-2 transition-all duration-450"
          onError={(e) => { (e.target as HTMLImageElement).src = '/src/assets/default-driver.png' }}
        />
      </div>
    </div>
  ))}
</div>
 */