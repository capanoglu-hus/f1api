
import React, { useEffect, useState } from 'react';
import type {RaceResult}  from '../components/type_files'; // interface dosyanın yolu
import {getRaces} from '../services/api';

const Races: React.FC = () => {
  const [races, setRaces] = useState<RaceResult[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchDrivers = async () => {
      try {
        const data = await getRaces();
        setRaces(data);
      } catch (error) {
        console.error("Sürücüler yüklenirken hata oluştu:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchDrivers();
  }, []);

  if (loading) return <div className="p-8 text-white">Yükleniyor...</div>;

  return (
    <div className="p-6 grid grid-cols-1 lg:grid-cols-2 xl:grid-cols-2 gap-6 bg-zinc-950">
  {races.map((race) => (
    <div 
      key={race.id} 
      className="group relative flex bg-zinc-900 border-l-4 border-red-600 rounded-lg overflow-hidden shadow-lg transition-all duration-300 hover:shadow-red-900/20"
    >
      {/* Sol İçerik Alanı */}
      <div className="flex-1 p-6">
        <div className="flex justify-between items-start mb-4">
          <div>
            <h3 className="text-2xl font-black text-white uppercase tracking-tight leading-none">
              {race.raceName}
            </h3>
            <span className="text-red-500 font-mono text-sm inline-block mt-2">
              {new Date(race.raceDate).toLocaleDateString('tr-TR', { day: '2-digit', month: 'long', year: 'numeric' })}
            </span>
          </div>
        </div>
        
        {/* Podyum Listesi */}
        <div className="space-y-3">
          <div className="flex items-center gap-3">
            <span className="w-8 h-8 flex items-center justify-center bg-yellow-500 text-black font-bold rounded-full text-xs italic">1</span>
            <div className="text-lg font-bold text-zinc-100 uppercase tracking-wide">{race.winner}</div>
          </div>
          <div className="flex items-center gap-3">
            <span className="w-8 h-8 flex items-center justify-center bg-zinc-400 text-black font-bold rounded-full text-xs italic">2</span>
            <div className="text-lg font-medium text-zinc-300 uppercase">{race.second}</div>
          </div>
          <div className="flex items-center gap-3">
            <span className="w-8 h-8 flex items-center justify-center bg-orange-700 text-white font-bold rounded-full text-xs italic">3</span>
            <div className="text-lg font-medium text-zinc-400 uppercase">{race.third}</div>
          </div>
        </div>
      </div>

      {/* Sağ Resim Alanı */}
      <div className="w-1/3 relative bg-zinc-800 transition-colors duration-500 group-hover:bg-blue-400">
        <img 
          src={`/src/assets/${race.imgUrl}`} // Verinizdeki yarış/pist görseli
          alt={race.raceName}
          className="w-full h-full object-contain mix-blend-multiply opacity-60 group-hover:opacity-100 transition-all duration-500 grayscale group-hover:grayscale-0"
        />
        
        
      </div>
    </div>
  ))}
</div>
  );
};

export default Races;