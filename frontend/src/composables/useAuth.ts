import { useRouter } from 'vue-router'
import { useAuthUserStore } from '@/stores/authUserStore'

export function useAuth() {
    const router = useRouter()
    const authUserStore = useAuthUserStore()

    const logout = () => {
        authUserStore.logout()
        router.push({ path: '/' }) // redireciona para login
    }
    const login = () => {
        authUserStore.login()
        router.push({ path: '/management-students' }) // redireciona para a página de gerenciamento de alunos
    }

    return { authUserStore, logout, login }
}
