<?php
namespace App\ExperienceDatabase;

use SilverStripe\Admin\ModelAdmin;
use App\CharacterDatabase\CharacterHat;
use App\CharacterDatabase\CharacterEyes;

/**
 * Class \App\Database\ExperienceAdmin
 *
 */
class CharacterAdmin extends ModelAdmin
{

    private static $managed_models = array (
        CharacterHat::class,
        CharacterEyes::class,
    );

    private static $url_segment = "logs";

    private static $menu_title = "Logs";

    private static $menu_icon = "app/client/icons/add.svg";

    public function init()
    {
        parent::init();
    }
}
