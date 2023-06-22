'use client'
import React from "react";
import { ChakraProvider, ColorModeScript } from '@chakra-ui/react'
import Theme from "./Theme";
import { SessionProvider } from 'next-auth/react'

export default function Provider({ children }: { children: React.ReactNode }) {
    return (
        <SessionProvider>
            <ChakraProvider>
                <ColorModeScript initialColorMode={Theme.config.initialColorMode} />
                {children}
            </ChakraProvider>
        </SessionProvider>
    )
}