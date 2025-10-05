/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import Index from '@/pages/index.vue'
import ManagementStudents from '@/pages/ManagementStudents.vue'
import { useAuthUserStore } from '@/store/authUserStore'
import { createRouter, createWebHistory, type RouteLocationRaw } from 'vue-router'


const routes = [
  { path: '/', name: 'home', component: Index },
  {
    path: '/management-students',
    name: 'management-students',
    component: ManagementStudents,
    meta: { requiresAuth: true } // 🔒 rota protegida
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach(async (to) => {
  const authUserStore = useAuthUserStore()

  const requiresAuth = to.meta.requiresAuth


  if (requiresAuth && !authUserStore.isAuthenticated) {

    const redirectLocation: RouteLocationRaw = { path: '/' }
    return redirectLocation
  }

  // Opcional: Impedir que usuários logados acessem a página de Login
  if ((to.path === '/') && authUserStore.isAuthenticated) {
    // Redireciona para uma página logada
    const redirectLocation: RouteLocationRaw = { path: '/management-students' }
    return redirectLocation
  }

})
export default router
