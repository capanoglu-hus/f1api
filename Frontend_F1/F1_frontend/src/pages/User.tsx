import React, { useEffect, useState } from 'react';
import type { UserInfo,UpdateUser}  from '../components/type_files'; // interface dosyanın yolu
import {getUserInfo, updateUser} from '../services/api';

const User: React.FC = () => {
const [Info, setInfo] = useState<UserInfo | null>(null);
const [formdata ,setFormdata] =useState<UpdateUser>({
        name : '',
        walletAddress :''
    });
const [loading, setLoading] = useState<boolean>(true);


const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  const { name, value } = e.target;
  setFormdata(prev => ({ ...prev, [name]: value }));
};

const handleSubmit = async (e: React.FormEvent) =>{
      e.preventDefault();
      console.log("giriş yapılıyor:" , formdata);
      await updateUser(formdata);
     window.location.reload(); 
        
}



useEffect(() => {
    const fetchInfos = async () => {
      try {
        const data = await getUserInfo();
        setInfo(data);
      } catch (error) {
        console.error("INFO yüklenirken hata oluştu:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchInfos();
  }, []);

if (loading) return <div className="p-8 text-white">Yükleniyor...</div>;

  return (
    <div className="flex flex-col md:flex-row min-h-screen bg-zinc-950 gap-4 p-4">
   
    <div className="w-full md:w-1/2">
     <h2 className="text-2xl font-black text-white mb-6 sticky top-0 bg-zinc-950 py-2 z-10 border-b border-blue-600">
    User <span className="text-blue-600">Infos</span>
  </h2>
      {Info ? ( // map yerine 'eğer veri varsa göster' kontrolü
        <div className="bg-zinc-900 border-t-4 border-red-600 p-5 rounded-xl shadow-lg">
          <h3 className="text-2xl font-black text-white"> {Info.name}</h3>
<div className="mt-2 text-2xl font-mono text-zinc-500"> email : {Info.email}</div>
          
                <div className="text-xs">
              
              <span className="text-white font-bold"> Wallet address : {Info.walletAddress}</span>
            </div>
                  <span className="text-red-500">
      {new Date(Info.createdDate).toLocaleDateString('tr-TR')}
    </span>
              
        </div>
      ) : (
        <p className="text-white">Yükleniyor...</p>
      )}
    </div>
     <div className="bg-zinc-900 p-8 rounded-xl border-l-4 border-red-600 shadow-2xl w-full max-w-md">
        <h2 className="text-3xl font-black text-white uppercase mb-6">Giriş Yap</h2>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="text-zinc-400 text-sm uppercase">name</label>
            <input 
              name="name"
              type="name" 
              onChange={handleChange}
              className="w-full bg-zinc-800 border border-zinc-700 p-3 rounded text-white focus:border-red-600 outline-none transition-all"
              placeholder="f1@pilot.com"
            />
          </div>
          <div>
            <label className="text-zinc-400 text-sm uppercase">walletAddress</label>
            <input 
              name="walletAddress"
              type="walletAddress" 
              onChange={handleChange}
              className="w-full bg-zinc-800 border border-zinc-700 p-3 rounded text-white focus:border-red-600 outline-none transition-all"
              placeholder="••••••••"
            />
          </div>
          
          <button className="w-full bg-white hover:bg-blue-400 text-black font-bold py-3 px-4 rounded transition-all mt-4 flex items-center justify-center">
           -
            <span className="uppercase tracking-widest">  güncelle  </span>
            
           -
          </button>
        </form>
      </div>
    </div>
  )
}

export default User