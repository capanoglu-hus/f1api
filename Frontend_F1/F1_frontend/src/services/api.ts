import axios from 'axios';
import type {DriverTypes,TeamTypes,RaceResult,
  BestTeam,BestDriver,UserInfo,AuthResponse,
  LoginRequest,RegisterRequest,DriverForVote,
  TeamForVote,RacePrediction,UpdateUser}  from '../components/type_files'; // interface dosyanın yolu
import {baseUrl,drivers ,teams,races,
   bestTeam,bestDriver,userInfo, login,
   register,driverforvote, DriverVoteBool,TeamVoteBool,
   teamforvote,racePrediction,RacePredictionBool,LastRaceInfo,UpdateUserInfo } from '../components/ApiLinks'; // Kendi URL'ini buraya yaz

const api = axios.create({
  baseURL: `${baseUrl}`
});

// İsteği gönderilmeden hemen önce yakala
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('accessToken');
  
  if (token) {
    // Tüm isteklere otomatik olarak Token ekler
    config.headers.Authorization = `Bearer ${token}`;
  }
  
  return config;
}, (error) => {
  return Promise.reject(error);
});

export const getDrivers = async (): Promise<DriverTypes[]> => {
    // <DriverTypes[]> diyerek Axios'a dönecek verinin bir sürücü listesi olduğunu söylüyoruz
    const response = await api.get<DriverTypes[]>(`${drivers}`);
    
    return response.data;
};

export const getTeams = async (): Promise<TeamTypes[]> => {
    // <DriverTypes[]> diyerek Axios'a dönecek verinin bir sürücü listesi olduğunu söylüyoruz
    const response = await api.get<TeamTypes[]>(`${teams}`);
    return response.data;
};

export const getRaces = async (): Promise<RaceResult[]> => {
    
    const response = await api.get<RaceResult[]>(`${races}`);
    return response.data;
};
export const getLastRacesInfo = async (): Promise<RaceResult> => {
    
    const response = await api.get<RaceResult>(`${LastRaceInfo}`);
    return response.data;
};
export const getBestTeams = async (): Promise<BestTeam | null> => {
   
    const response = await api.get<BestTeam | null>(`${bestTeam}`);
    return response.data;
};

export const getBestDrivers = async (): Promise<BestDriver[]> => {
   
    const response = await api.get<BestDriver[]>(`${bestDriver}`);
    return response.data;
};

export const getUserInfo = async (): Promise<UserInfo | null> =>{
    const response = await api.get<UserInfo | null>(`${userInfo}`)
    return response.data;
}

export const loginService = async (data: LoginRequest) : Promise<AuthResponse> => {
    const response= await fetch(`${login}`,{
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data),
    })

    if(!response.ok) throw new Error('Giriş başarısız!');
    const result: AuthResponse = await response.json();
    localStorage.setItem('accessToken', result.accessToken);
    localStorage.setItem('refreshToken', result.refreshToken);
  
  return result;
}

export const registerService = async (data: RegisterRequest) : Promise<AuthResponse> => {
    const response= await fetch(`${register}`,{
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data),
    })
    if(!response.ok) throw new Error('kayıt başarısız!');
    const result: AuthResponse = await response.json();
  return result;
}

export const driverVoteBool = async (): Promise<boolean> =>{
  const response = await api.get<boolean>(`${DriverVoteBool}`);
  return response.data;
}

export const teamVoteBool = async (): Promise<boolean> =>{
  const response = await api.get<boolean>(`${TeamVoteBool}`);
  return response.data;
}

export const driverVote = async (data: DriverForVote) : Promise<boolean> => {
    const response= await api.post(`${driverforvote}`,data)

    if(!response.data) throw new Error('OY GEÇERSİZ');
    
  return response.data;
}

export const teamVote = async (data: TeamForVote) : Promise<boolean> => {
    const response= await api.post(`${teamforvote}`,data)

    if(!response.data) throw new Error('OY GEÇERSİZ');
    
  return response.data;
}

export const prediction = async (data: RacePrediction) : Promise<boolean> => {
    const response= await api.post(`${racePrediction}`,data)

    if(!response.data) throw new Error('OY GEÇERSİZ');
    
  return response.data;
}

export const racePredictionBool = async (): Promise<boolean> =>{
  const response = await api.get<boolean>(`${RacePredictionBool}`);
  return response.data;
}

export const updateUser = async(data : UpdateUser): Promise<boolean> => {
   const response= await api.post(`${UpdateUserInfo}`,data)

    if(!response.data) throw new Error('OY GEÇERSİZ');
    
  return response.data;
}