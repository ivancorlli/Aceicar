'use client'
import { User, UserStatus } from '@/core/User/Index';
import { useUser } from '@auth0/nextjs-auth0/client';
import React, { createContext,useEffect,useState } from 'react'


export const UserContext = createContext<{user:User | null, isLoading:boolean}>({user:null,isLoading:false});

const UserProvider = ({ children}: { children: React.ReactNode}) => {
    const { user: session, isLoading } = useUser();
    const [user, setUser] = useState<User | null>(null)

    useEffect(()=>{
        if(session?.sub != null)
        {
            const user:User = {
                UserId: session.sub!,
                Email: session.email!,
                Status: UserStatus.Active,
                CreatedAt: Date.now(),
                UpdatedAt: Date.now(),
            }
            if (session.picture != null) user.ProfileImage = session.picture
            setUser(user)
        }else {
            setUser(null)
        }
    },[session])
    const value = {
        user,
        isLoading,
    }
    return (
        <UserContext.Provider value={{...value}}>
            {children}
        </UserContext.Provider>
    );
};

export default UserProvider