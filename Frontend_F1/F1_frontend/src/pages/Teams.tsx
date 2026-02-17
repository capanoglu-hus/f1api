
import React, { useEffect, useState } from 'react';
import type {TeamTypes ,TeamForVote}  from '../components/type_files'; // interface dosyanın yolu
import {getTeams,teamVoteBool,teamVote} from '../services/api';

const Teams: React.FC = () => {
  const [teams, setTeams] = useState<TeamTypes[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [bool ,setBool] = useState<boolean>(false);

  useEffect(() => {
    const fetchDrivers = async () => {
      try {
        const data = await getTeams();
        setTeams(data);
      } catch (error) {
        console.error("Sürücüler yüklenirken hata oluştu:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchDrivers();
  }, []);

    useEffect(() => {
      const teamVotebool = async () => {
        try {
          const data = await teamVoteBool();
          setBool(data);
        } catch (error) {
          console.error("Sürücüler yüklenirken hata oluştu:", error);
        } finally {
          setLoading(false);
        }
      };
  
      teamVotebool();
    }, []);

     const [formdata, setFormdata] = useState<TeamForVote>({
        teamId: 0,
      });

      const handleSubmit = async (e: React.FormEvent) =>{
        e.preventDefault();
        console.log("OY VERİLDİ:" , formdata);
        await teamVote(formdata);
        window.location.reload(); 
      }
      
      const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormdata(prev => ({ ...prev, [name]: Number(value) }));
          
      };

  if (loading) return <div className="p-8 text-white">Yükleniyor...</div>;

  return (
    <div className="min-h-screen bg-zinc-950 p-6">
  {/* 1. OYLAMA BÖLÜMÜ (Günün Takımı) */}
{bool && (
  <div className="mb-12 animate-in fade-in slide-in-from-top duration-700">
    <div className="max-w-4xl mx-auto bg-zinc-900 border border-zinc-800 rounded-3xl p-8 shadow-2xl">
      <h2 className="text-2xl font-black text-white uppercase tracking-tighter mb-6 flex items-center gap-2">
        <span className="w-2 h-8 bg-red-600 rounded-full"></span>
        Günün Takımını Seç
      </h2>
      
      <form onSubmit={handleSubmit} className="flex flex-col gap-6">
        <div className="space-y-2">
          <label className="text-zinc-500 text-xs font-bold uppercase ml-1">Takım Seçimi</label>
          <div className="relative">
            <select 
              name="teamId"
              onChange={handleChange}
              // value={formdata.teamId} // State yönetimi için
              className="w-full bg-zinc-800 border border-zinc-700 p-4 rounded-xl text-white focus:border-red-600 focus:ring-1 focus:ring-red-600 outline-none appearance-none cursor-pointer transition-all"
            >
              <option value="0">Bir Takım Seçiniz...</option>
              {teams?.map((team) => (
                <option key={team.id} value={team.id} className="bg-zinc-900">
                   { team.name} {/* API'den gelen isimlendirmeye göre */}
                </option>
              ))}
            </select>
            
            {/* Özel Ok İkonu */}
            <div className="absolute inset-y-0 right-4 flex items-center pointer-events-none">
              <svg className="w-4 h-4 text-zinc-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" />
              </svg>
            </div>
          </div>
        </div>

        <div className="w-full">
          <button className="w-full bg-red-600 hover:bg-red-700 text-white font-black py-4 rounded-xl transition-all uppercase tracking-[0.2em] shadow-lg shadow-red-900/20 active:scale-[0.98]">
            Oylamayı Tamamla
          </button>
        </div>
      </form>
    </div>
  </div>
)}
  <div className="p-8 flex flex-col gap-10 bg-zinc-950">
  {teams.map((team) => (
    <div 
      key={team.id} 
      className="group relative flex flex-col md:flex-row bg-black border-l-4 border-red-600 rounded-xl overflow-hidden shadow-[0_0_20px_rgba(0,0,0,0.5)] transition-all duration-300 hover:bg-blue-950"
    >
      {/* Sol İçerik Alanı */}
      <div className="flex-1 p-8 z-10">
        <h2 className="text-5xl font-black text-white uppercase tracking-tighter leading-none mb-2 group-hover:text-red-600 transition-colors">
          {team.name}
        </h2>
        <br></br>
        <p className="text-2xl text-zinc-300 font-semibold mb-8 flex items-center gap-2">
          <span className="text-zinc-500 font-normal uppercase text-sm tracking-widest">Principal:</span> 
          <span className="text-white group-hover:text-red-100 ">{team.principal}</span>
        </p>
        <br></br>
        {/* Driver Kartları Yan Yana */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 max-w-3xl">
          {team.drivers.map((driver) => (
            <div 
              key={driver.id} 
              className="bg-zinc-950/50 p-5 rounded-lg border border-zinc-700/50 hover:border-orange-200 transition-all group/card"
            >
              <div className="flex justify-between items-center">
                <div>
                  <h3 className="text-lg font-bold text-zinc-100 hover:border-red-600/50 transition-colors">
                    {driver.name}
                  </h3>
                  <p className="text-xs text-zinc-100 font-medium uppercase tracking-wider mt-1 hover:border-red-600/50">
                    {driver.description}
                  </p>
                </div>
                <span className="text-4xl font-mono font-black text-red-600 transition-colors hover:border-red-600/50">
                  {driver.racingNumber}
                </span>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Sağ Takım Görseli / Logosu */}
      <div className="relative w-full md:w-2/5 min-h-62.5 flex items-center justify-center p-6 bg-zinc-800/30">
        <img 
          src={`/src/assets/${team.imgUrl}`} 
          alt={team.name}
          className="w-250 h-100 object-contain transition-transform duration-500 group-hover:scale-105"
        />
        
        {/* Sol taraftaki karanlık geçişi yumuşattık */}
        <div className="absolute inset-0 bg-linear-to-r from-zinc-900 via-transparent to-transparent hidden md:block w-1/4"></div>
      </div>
    </div>
  ))}
</div>
</div>
  );
};

export default Teams;

/*
  
  {bool && (
    <div className="mb-12 animate-in fade-in slide-in-from-top duration-700">
      <div className="max-w-4xl mx-auto bg-zinc-900 border border-zinc-800 rounded-3xl p-8 shadow-2xl">
        <h2 className="text-2xl font-black text-white uppercase tracking-tighter mb-6 flex items-center gap-2">
          <span className="w-2 h-8 bg-red-600 rounded-full"></span>
          Günün takım Seç
        </h2>
        
        <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-3 gap-6">
          
       
          <div className="space-y-2">
            <label className="text-zinc-500 text-xs font-bold uppercase ml-1"> takım ID</label>
            <input 
              name="teamId"
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
  )}*/