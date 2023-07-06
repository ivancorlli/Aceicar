'use client'
import React from 'react'
import { UserProvider } from '@auth0/nextjs-auth0/client';
import { QueryClient, QueryClientProvider } from 'react-query';

type Props = {
  children: React.ReactNode
}

const queryClient = new QueryClient()
const NextAuth = ({ children }: Props) => {
  return (
    <>
      <UserProvider>
        <QueryClientProvider client={queryClient}>

          {children}
        </QueryClientProvider>
      </UserProvider>
    </>
  )
}

export default NextAuth