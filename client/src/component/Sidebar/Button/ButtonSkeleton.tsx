import { HStack, Skeleton} from '@chakra-ui/react'
import React from 'react'

const ButtonSkeleton = () => {
  return (
    <HStack w="100%" h="100%" alignItems="center">
        <Skeleton w="100%" h="25px" borderRadius={6}>
            <div>data</div>
        </Skeleton>    
    </HStack>
  )
}

export default ButtonSkeleton