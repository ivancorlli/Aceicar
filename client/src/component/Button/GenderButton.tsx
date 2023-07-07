import { UserGender } from '@/lib/enum/UserGender'
import { Button, HStack } from '@chakra-ui/react'
import React, { MouseEvent } from 'react'


const GenderButton = ({ handleSelect, selectedGender }: { handleSelect: (e: MouseEvent<HTMLButtonElement>) => void, selectedGender: string }) => {

    return (
        <HStack w="100%">
            {
                selectedGender === UserGender.Male ?
                    <>
                        <Button id={UserGender.Male} name={UserGender.Male} onClick={(e) => handleSelect(e)} bg="brand.100" color="white" w="100%">
                            Masculino
                        </Button>
                        <Button id={UserGender.Female} name={UserGender.Female} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                            Femenino
                        </Button>
                    </>
                    :

                    selectedGender === UserGender.Female ?

                        <>
                            <Button id={UserGender.Male} name={UserGender.Male} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                                Masculino
                            </Button>
                            <Button id={UserGender.Female} name={UserGender.Female} onClick={(e) => handleSelect(e)} bg="brand.100" color="white" w="100%">
                                Femenino
                            </Button>
                        </>
                        :
                        <>
                            <Button id={UserGender.Male} name={UserGender.Male} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                                Masculino
                            </Button>
                            <Button id={UserGender.Female} name={UserGender.Female} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                                Femenino
                            </Button>
                        </>
            }
        </HStack>
    )
}

export default GenderButton