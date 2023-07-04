'use client'

import { HStack } from '@chakra-ui/react'
import React from 'react'
import MobileMenu from '../Menu/MobileMenu'

const MobileNavbar = () => {
  return (
    <HStack
    display={["block","none"]}
    position="sticky"
    top="0"
    left="0"
    >
        <MobileMenu/>
    </HStack>
  )
}

export default MobileNavbar