import { Button, HStack } from '@chakra-ui/react'
import React, { MouseEvent, useState } from 'react'

enum Gender {
    Male = "0",
    Female = "1"
}

const GenderButton = ({ handleSelect, selectedGender }: { handleSelect: (e: MouseEvent<HTMLButtonElement>) => void, selectedGender: string }) => {

    return (
        <HStack w="100%">
            {
                selectedGender === Gender.Male ?
                    <>
                        <Button id={Gender.Male} name={Gender.Male} onClick={(e) => handleSelect(e)} bg="brand.100" color="white" w="100%">
                            Masculino
                        </Button>
                        <Button id={Gender.Female} name={Gender.Female} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                            Femenino
                        </Button>
                    </>
                    :

                    selectedGender === Gender.Female ?

                        <>
                            <Button id={Gender.Male} name={Gender.Male} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                                Masculino
                            </Button>
                            <Button id={Gender.Female} name={Gender.Female} onClick={(e) => handleSelect(e)} bg="brand.100" color="white" w="100%">
                                Femenino
                            </Button>
                        </>
                        :
                        <>
                            <Button id={Gender.Male} name={Gender.Male} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                                Masculino
                            </Button>
                            <Button id={Gender.Female} name={Gender.Female} onClick={(e) => handleSelect(e)} bg="white" borderColor="brand.100" borderWidth="2px" w="100%">
                                Femenino
                            </Button>
                        </>
            }
        </HStack>
    )
}

export default GenderButton