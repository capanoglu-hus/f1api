
import React, { useEffect, useState } from 'react';
import type { BestDriver, BestTeam}  from '../components/type_files'; // interface dosyanın yolu
import {getBestDrivers, getBestTeams} from '../services/api';


const Home: React.FC = () => {
const [bestTeam, setBestTeam] = useState<BestTeam | null>(null);
const [bestdriver,setBestDriver] = useState<BestDriver[]>();
const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchTeams = async () => {
      try {
        const data = await getBestTeams();
        setBestTeam(data);
      } catch (error) {
        console.error("team yüklenirken hata oluştu:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchTeams();
  }, []);

  useEffect(() => {
    const fetchbestDriver = async () => {
      try {
        const data2 = await getBestDrivers();
        setBestDriver(data2);
      } catch (error) {
        console.error("Sürücüler yüklenirken hata oluştu:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchbestDriver();
  }, []);

  if (loading) return <div className="p-8 text-white">Yükleniyor...</div>;

return (
  <div className="flex flex-col md:flex-row min-h-screen bg-zinc-950 gap-4 p-4">
   
    <div className="w-full md:w-1/2">
     <h2 className="text-2xl font-black text-white mb-6 sticky top-0 bg-zinc-950 py-2 z-10 border-b border-blue-600">
      BEST <span className="text-blue-600">TEAMS</span>
      </h2>
      {bestTeam ? ( // map yerine 'eğer veri varsa göster' kontrolü
        <div className="bg-zinc-900 border-t-4 border-red-600 p-5 rounded-xl shadow-lg">
          <h3 className="text-2xl font-black text-white">{bestTeam.teamName}</h3>
<div className="mt-2 text-2xl font-mono text-zinc-500"> Principal: {bestTeam.teamPrincipal}</div>
          
          
          <div className="space-y-2">
            {/* Takımın içindeki sürücüler hala bir liste, burada map devam eder */}
            {bestTeam.drivers?.map((driver, index) => (
              <div key={index} className="bg-zinc-800/40 p-3 rounded flex justify-between">
                <span className="text-zinc-200">{driver.name}</span>
                <span className="text-red-500 font-mono">#{driver.racingNumber}</span>
              </div>
            ))}
          </div>
          
                <div className="text-xs">
              <span className="text-zinc-500 block">Votes</span>
              <span className="text-white font-bold">{bestTeam.totalVotes}</span>
            </div>
                  <span className="text-red-500">
      {new Date(bestTeam.voteUpdateDate).toLocaleDateString('tr-TR')}
    </span>
              
        </div>
      ) : (
        <p className="text-white">Yükleniyor...</p>
      )}
    </div>
    
    <div className="w-full md:w-1/2 ">
  <h2 className="text-2xl font-black text-white mb-6 sticky top-0 bg-zinc-950 py-2 z-10 border-b border-blue-600">
    BEST <span className="text-blue-600">DRIVERS</span>
  </h2>

  {/* Doğrudan bestDriver listesi üzerinde dönüyoruz */}
  <div className="flex flex-col gap-4">
    {bestdriver?.map((driver) => (
        <div key={driver.id} className="bg-zinc-900 border-l-4 border-red-600 p-4 rounded shadow-md">
          <h3 className="text-xl font-bold text-white">{driver.driverName}</h3>
        
          
          <div className="mt-2 text-2xl font-mono text-zinc-500"> {driver.racingNumber}</div>
          <div className="mt-2 text-2xl font-mono text-zinc-500"> {driver.teamName}</div>
          <div className="mt-2 text-2xl font-mono text-zinc-500"> {driver.description}</div>
          <div className="flex gap-4">
            <div className="text-xs">
              <span className="text-zinc-500 block">Score</span>
              <span className="text-white font-bold">{driver.totalScore}</span>
            </div>
            <div className="text-xs">
              <span className="text-zinc-500 block">Votes</span>
              <span className="text-white font-bold">{driver.totalVotes}</span>
            </div>
            
             <span className="text-red-500">
      {new Date(driver.ratedDate).toLocaleDateString('tr-TR')}
    </span>
          </div>
        </div>
      ))}
  </div>
</div>
  </div>
);
};

export default Home;