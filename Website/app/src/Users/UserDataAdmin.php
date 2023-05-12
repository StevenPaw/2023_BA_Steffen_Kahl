<?php
namespace App\Users;

use SilverStripe\Admin\ModelAdmin;
use App\Users\UserData;

/**
 * Class \App\Database\ExperienceAdmin
 *
 */
class UserDataAdmin extends ModelAdmin
{
    private static $managed_models = array (
        UserData::class,
    );

    private static $url_segment = "userdata-list";

    private static $menu_title = "User Data";

    private static $menu_icon = "app/client/icons/block-text.svg";
}
