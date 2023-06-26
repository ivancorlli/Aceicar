'use client'
import React from "react";
import { ChakraProvider, ColorModeScript } from '@chakra-ui/react'
import Theme from "./Theme";

export default function Provider({ children}: { children: React.ReactNode}) {
    return (
            <ChakraProvider theme={Theme}>
                <ColorModeScript initialColorMode={Theme.config.initialColorMode} />
                {children}
            </ChakraProvider>
    )
}