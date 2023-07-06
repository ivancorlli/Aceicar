import User from '@/lib/entity/User'
import { useUser } from '@auth0/nextjs-auth0/client'
import { useQuery } from 'react-query'

async function fetcher():Promise<User>
{
  const res = await fetch("/api/user/getMe")
  const data:User = await res.json()
  return data
}


const  useSession= () => {
  const {user:session,isLoading,error} = useUser()
  const query = useQuery({
    queryKey:"user",
    queryFn :fetcher
  })
  if(session)
  {
    if(query.data == undefined)
    {
      return {
        session:true,
        user:null,
        error:query.error,
        isLoading:query.isLoading,
      }
    }else {
      return {
        session:true,
        user:query.data,
        error:query.error,
        isLoading:query.isLoading,
      } 
    }

  }else {
    return {
      session:false,
      user:null,
      error,
      isLoading
    }
  }
}

export default useSession