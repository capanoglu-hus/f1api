
import React, {  useEffect, useState } from 'react';
import type {RacePrediction,RaceResult}  from '../components/type_files'; // interface dosyanın yolu
import {prediction,racePredictionBool,getLastRacesInfo} from '../services/api';

const Prediction: React.FC = () => {
 const [bool ,setBool] = useState<boolean>(false);
 const [timeLeft, setTimeLeft] = useState({ gun: 0, saat: 0, dk: 0, sn: 0 });
 const [races, setRaces] = useState<RaceResult>();

useEffect(() => {
  if (!races?.raceDate) return;

   const timer = setInterval(() => {
    const now = new Date().getTime();
    const distance = new Date(races.raceDate).getTime() - now;

    if (distance < 0) {
      clearInterval(timer);
    } else {
      setTimeLeft({
        gun: Math.floor(distance / (1000 * 60 * 60 * 24)),
        saat: Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)),
        dk: Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60)),
        sn: Math.floor((distance % (1000 * 60)) / 1000),
      });
    }
  }, 1000);

  return () => clearInterval(timer);
}, [races]);

  useEffect(() => {
    const fetchDrivers = async () => {
      try {
        const data = await getLastRacesInfo();
        setRaces(data);
      } catch (error) {
        console.error("Sürücüler yüklenirken hata oluştu:", error);
      } 
    };

    fetchDrivers();
  }, []);



  useEffect(() => {
      const driverVotebool = async () => {
        try {
          const data = await racePredictionBool();
          setBool(data);
        } catch (error) {
          console.error("Sürücüler yüklenirken hata oluştu:", error);
        }
      };
  
      driverVotebool();
    }, []);


  const [formdata, setFormdata] = useState<RacePrediction>({
    raceId: 0,
    firstPlaceDriverId: 0,
    secondPlaceDriverId: 0,
    thirdPlaceDriverId: 0,
  });

 

  const handleSubmit = async (e: React.FormEvent) =>{
    e.preventDefault();
    console.log("OY VERİLDİ:" , formdata );
    await prediction(formdata);
    window.location.reload(); 
  }
  
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
     if (races?.id) {
          setFormdata(prev => ({ ...prev,raceId: races.id, [name]: Number(value) }));
        }
    setFormdata(prev => ({ ...prev , [name]: Number(value) }));
  };

  return (
 <div className="min-h-screen bg-zinc-950 p-4 md:p-8">
  {/* 1. YARIŞ HERO ALANI & GERİ SAYIM */}
  <div className="max-w-6xl mx-auto mb-10">
    {races ? (
      <div className="relative overflow-hidden bg-zinc-900 border border-zinc-800 rounded-3xl p-8 shadow-2xl">
        {/* Arka Plan Pist Resmi (Hafif Transparan) */}
        <div className="absolute right-0 top-0 w-1/2 h-full opacity-10 pointer-events-none">
          <img 
            src={`/src/assets/${races.imgUrl}`} 
            alt="track" 
            className="w-full h-full object-contain transform rotate-12 scale-150"
          />
        </div>

        <div className="relative z-10 grid grid-cols-1 md:grid-cols-2 gap-8 items-center">
          {/* Sol: Yarış Bilgileri */}
          <div>
            <div className="flex items-center gap-3 mb-4">
              <span className="bg-red-600 text-white text-xs font-black px-3 py-1 rounded-full uppercase tracking-widest">
                Sıradaki Yarış
              </span>
              <span className="text-zinc-500 font-bold">
                {new Date(races.raceDate).toLocaleDateString('tr-TR', { day: 'numeric', month: 'long', year: 'numeric' })}
              </span>
            </div>
            <h1 className="text-5xl md:text-7xl font-black text-white uppercase italic tracking-tighter mb-4">
              {races.raceName}
            </h1>
            
            {/* Sayaç Kutuları */}
            <div className="flex gap-4 mt-8">
              {[
                { label: 'GÜN', value: timeLeft.gun },
                { label: 'SAAT', value: timeLeft.saat },
                { label: 'DK', value: timeLeft.dk },
                { label: 'SN', value: timeLeft.sn }
              ].map((item, index) => (
                <div key={index} className="flex flex-col items-center">
                  <div className="bg-zinc-800 border border-zinc-700 w-16 h-16 md:w-20 md:h-20 rounded-2xl flex items-center justify-center shadow-xl">
                    <span className="text-2xl md:text-3xl font-black text-red-600 leading-none">
                      {String(item.value).padStart(2, '0')}
                    </span>
                  </div>
                  <span className="text-[10px] font-bold text-zinc-500 mt-2 tracking-widest">{item.label}</span>
                </div>
              ))}
            </div>
          </div>

          {/* Sağ: Pist Görseli */}
          <div className="hidden md:flex justify-end">
            <img 
              src={`/src/assets/${races.imgUrl}`} 
              alt={races.raceName}
              className="w-4/5 drop-shadow-[0_0_30px_rgba(220,38,38,0.2)] hover:scale-105 transition-transform duration-500"
              onError={(e) => { (e.target as HTMLImageElement).src = '/src/assets/Istanbul.png' }}
            />
          </div>
        </div>
      </div>
    ) : (
      <div className="flex justify-center p-20">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-red-600"></div>
      </div>
    )}
  </div>

  {/* 2. TAHMİN FORMU */}
  {bool && races && (
    <div className="max-w-4xl mx-auto animate-in fade-in slide-in-from-bottom duration-700 delay-200">
      <div className="bg-zinc-900 border border-zinc-800 rounded-3xl p-8 shadow-2xl relative overflow-hidden">
        {/* Dekoratif Çizgi */}
        <div className="absolute top-0 left-0 w-full h-1 bg-linear-to-r from-transparent via-red-600 to-transparent"></div>
        
        <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
          <h2 className="text-3xl font-black text-white uppercase italic tracking-tighter">
            Podyum Tahminini Yap
          </h2>
          <div className="text-zinc-500 text-sm font-bold bg-zinc-800 px-4 py-2 rounded-lg border border-zinc-700">
            Race ID: <span className="text-red-500">#{races.id}</span>
          </div>
        </div>

        <form onSubmit={handleSubmit} className="space-y-6">
          {/* RaceID'yi input olarak değil, gizli gönderiyoruz veya state'te tutuyoruz */}
          <input type="hidden" name="raceId" value={races.id} />

          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            {/* Podyum Sıralaması */}
            {[
              { id: "firstPlaceDriverId", label: "1. SIRA (P1)", color: "border-yellow-500", icon: "🥇" },
              { id: "secondPlaceDriverId", label: "2. SIRA (P2)", color: "border-zinc-400", icon: "🥈" },
              { id: "thirdPlaceDriverId", label: "3. SIRA (P3)", color: "border-orange-600", icon: "🥉" }
            ].map((pos) => (
              <div key={pos.id} className="group flex flex-col gap-2">
                <label className="text-zinc-400 text-[10px] font-black uppercase tracking-[0.2em] ml-1">
                  {pos.icon} {pos.label}
                </label>
                <input 
                  name={pos.id}
                  type="number" 
                  onChange={handleChange}
                  className={`w-full bg-zinc-950 border-2 ${pos.color} bg-opacity-50 p-4 rounded-2xl text-white font-bold focus:ring-4 focus:ring-red-600/20 outline-none transition-all placeholder:text-zinc-800`}
                  placeholder="Sürücü ID Gir"
                />
              </div>
            ))}
          </div>

          <button className="group relative w-full overflow-hidden bg-white hover:bg-red-600 text-black hover:text-white font-black py-5 rounded-2xl transition-all duration-300 uppercase tracking-[0.3em] text-sm">
            <span className="relative z-10">TAHMİNİ GÖNDER</span>
            <div className="absolute inset-0 bg-red-600 translate-y-full group-hover:translate-y-0 transition-transform duration-300"></div>
          </button>
        </form>
      </div>
    </div>
  )}
</div>

  );
};

export default Prediction;
